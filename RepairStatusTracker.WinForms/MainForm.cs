using System.Configuration;
using RepairStatusTracker.Shared.Models;
using RepairStatusTracker.WinForms.Helpers;
using RepairStatusTracker.WinForms.Services;

namespace RepairStatusTracker.WinForms;

internal sealed class MainForm : Form
{
    private readonly DataGridView dgvJobs = new();
    private readonly Button btnRefresh = new();
    private readonly Button btnUpdateStatus = new();
    private readonly BindingSource jobsBindingSource = new();
    private readonly ApiClient apiClient;

    public MainForm()
    {
        var apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] 
            ?? throw new InvalidOperationException("ApiBaseUrl not configured in App.config");

        apiClient = new ApiClient(new HttpClient
        {
            BaseAddress = new Uri(apiBaseUrl)
        });

        Text = "Repair Status Tracker";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(900, 600);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(12)
        };

        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        dgvJobs.Name = nameof(dgvJobs);
        dgvJobs.Dock = DockStyle.Fill;
        dgvJobs.AllowUserToAddRows = false;
        dgvJobs.AllowUserToDeleteRows = false;
        dgvJobs.ReadOnly = true;
        dgvJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvJobs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvJobs.MultiSelect = false;
        dgvJobs.DataSource = jobsBindingSource;
        dgvJobs.DataBindingComplete += (_, _) => ApplyRowColors();

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true,
            Padding = new Padding(0, 12, 0, 0)
        };

        btnRefresh.Name = nameof(btnRefresh);
        btnRefresh.Text = "Refresh";
        btnRefresh.AutoSize = true;

        btnUpdateStatus.Name = nameof(btnUpdateStatus);
        btnUpdateStatus.Text = "Update Status";
        btnUpdateStatus.AutoSize = true;
        btnUpdateStatus.Click += async (_, _) => await UpdateSelectedJobStatusAsync();

        buttonPanel.Controls.Add(btnUpdateStatus);
        buttonPanel.Controls.Add(btnRefresh);

        Load += async (_, _) => await LoadJobsAsync();
        btnRefresh.Click += async (_, _) => await LoadJobsAsync();

        layout.Controls.Add(dgvJobs, 0, 0);
        layout.Controls.Add(buttonPanel, 0, 1);

        Controls.Add(layout);
    }

    private async Task LoadJobsAsync()
    {
        try
        {
            btnRefresh.Enabled = false;
            btnUpdateStatus.Enabled = false;
            Cursor = Cursors.WaitCursor;

            var jobs = await apiClient.GetRepairJobsAsync();
            jobsBindingSource.DataSource = jobs.ToList();
            ApplyRowColors();
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(this,
                $"Failed to connect to the server.\n\n{ex.Message}",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this,
                $"Failed to load repair jobs.\n\n{ex.Message}",
                "Load Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
            btnRefresh.Enabled = true;
            btnUpdateStatus.Enabled = true;
        }
    }

    private void ApplyRowColors()
    {
        foreach (DataGridViewRow row in dgvJobs.Rows)
        {
            if (row.DataBoundItem is not RepairJob repairJob)
            {
                continue;
            }

            row.DefaultCellStyle.BackColor = StatusColorHelper.GetColor(repairJob.Status);
        }
    }

    private async Task UpdateSelectedJobStatusAsync()
    {
        if (dgvJobs.CurrentRow?.DataBoundItem is not RepairJob selectedJob)
        {
            MessageBox.Show(this, "Please select a repair job before updating its status.", "No Repair Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new RepairStatusDialog(selectedJob.Status);
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        if (!dialog.TryGetSelectedStatus(out var newStatus))
        {
            MessageBox.Show(this, "Please choose a repair status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var updated = await apiClient.UpdateStatusAsync(selectedJob.Id, newStatus.ToString());
            if (!updated)
            {
                MessageBox.Show(this, "The repair job could not be found or updated.", "API Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadJobsAsync();
            MessageBox.Show(this, "The repair job status was updated successfully.", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"The status update failed.\n\n{ex.Message}", "API Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
