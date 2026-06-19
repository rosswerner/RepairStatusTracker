using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;

namespace RepairStatusTracker.WinForms;

internal sealed class MainForm : Form
{
    private readonly DataGridView dgvJobs = new();
    private readonly Button btnRefresh = new();
    private readonly Button btnUpdateStatus = new();

    public MainForm()
    {
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

        layout.Controls.Add(dgvJobs, 0, 0);
        layout.Controls.Add(buttonPanel, 0, 1);

        Controls.Add(layout);
    }
}
