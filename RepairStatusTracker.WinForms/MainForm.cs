using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;

namespace RepairStatusTracker.WinForms;

internal sealed class MainForm : Form
{
    public MainForm()
    {
        var sampleTicket = new RepairTicket(1, "Sample repair", RepairStatus.Received);

        Text = "Repair Status Tracker";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(360, 160);

        Controls.Add(new Label
        {
            AutoSize = true,
            Left = 20,
            Top = 20,
            Text = $"{sampleTicket.Title} - {sampleTicket.Status}"
        });
    }
}
