using RepairStatusTracker.Shared.Enums;

namespace RepairStatusTracker.WinForms;

internal sealed class RepairStatusDialog : Form
{
    private readonly ComboBox cboRepairStatuses = new();
    private readonly Button btnOk = new();
    private readonly Button btnCancel = new();

    public RepairStatusDialog(RepairStatus? initialStatus = null)
    {
        Text = "Select Repair Status";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(320, 140);

        if (initialStatus.HasValue)
        {
            Shown += (_, _) => SetSelectedStatus(initialStatus.Value);
        }

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(12)
        };

        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        cboRepairStatuses.Name = nameof(cboRepairStatuses);
        cboRepairStatuses.Dock = DockStyle.Top;
        cboRepairStatuses.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRepairStatuses.DataSource = Enum.GetValues<RepairStatus>();

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true,
            Padding = new Padding(0, 12, 0, 0)
        };

        btnOk.Name = nameof(btnOk);
        btnOk.Text = "OK";
        btnOk.DialogResult = DialogResult.OK;
        btnOk.AutoSize = true;

        btnCancel.Name = nameof(btnCancel);
        btnCancel.Text = "Cancel";
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.AutoSize = true;

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        buttonPanel.Controls.Add(btnCancel);
        buttonPanel.Controls.Add(btnOk);

        layout.Controls.Add(cboRepairStatuses, 0, 0);
        layout.Controls.Add(buttonPanel, 0, 1);

        Controls.Add(layout);
    }

    public void SetSelectedStatus(RepairStatus status)
    {
        var values = Enum.GetValues<RepairStatus>();
        var index = Array.IndexOf(values, status);
        if (index >= 0)
        {
            cboRepairStatuses.SelectedIndex = index;
        }
    }

    public bool TryGetSelectedStatus(out RepairStatus status)
    {
        if (cboRepairStatuses.SelectedItem is RepairStatus selectedStatus)
        {
            status = selectedStatus;
            return true;
        }

        status = default;
        return false;
    }
}