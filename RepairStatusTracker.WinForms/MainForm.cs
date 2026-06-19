using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;
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
        apiClient = new ApiClient(new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/")
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
        var jobs = await apiClient.GetRepairJobsAsync();
        jobsBindingSource.DataSource = jobs.ToList();
    }
}
