using RepairStatusTracker.Shared.Enums;

namespace RepairStatusTracker.Shared.Validation;

/// <summary>
/// Validation helper for RepairStatus enum
/// </summary>
public static class RepairStatusValidator
{
    /// <summary>
    /// Validates and parses a status string to a RepairStatus enum value
    /// </summary>
    /// <param name="status">The status string to validate</param>
    /// <param name="result">The parsed RepairStatus enum value if validation succeeds</param>
    /// <returns>True if the status is valid and can be parsed; otherwise false</returns>
    public static bool IsValid(string? status, out RepairStatus result)
    {
        result = default;
        return !string.IsNullOrWhiteSpace(status) &&
               Enum.TryParse<RepairStatus>(status, ignoreCase: true, out result) &&
               Enum.IsDefined(typeof(RepairStatus), result);
    }
}
