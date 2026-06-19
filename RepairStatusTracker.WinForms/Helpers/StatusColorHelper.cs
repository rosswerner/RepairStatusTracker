using RepairStatusTracker.Shared.Enums;

namespace RepairStatusTracker.WinForms.Helpers;

/// <summary>
/// Helper class for mapping RepairStatus values to display colors
/// </summary>
public static class StatusColorHelper
{
    /// <summary>
    /// Gets the display color for a given repair status
    /// </summary>
    /// <param name="status">The repair status</param>
    /// <returns>The color to use for displaying the status</returns>
    public static Color GetColor(RepairStatus status)
    {
        return status switch
        {
            RepairStatus.Received => Color.LightGray,
            RepairStatus.InProgress => Color.LightBlue,
            RepairStatus.WaitingOnParts => Color.LightYellow,
            RepairStatus.QualityCheck => Color.Plum,
            RepairStatus.ReadyForPickup => Color.LightGreen,
            RepairStatus.Completed => Color.Green,
            _ => SystemColors.Window
        };
    }
}
