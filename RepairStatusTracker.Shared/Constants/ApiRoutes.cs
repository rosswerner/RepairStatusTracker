namespace RepairStatusTracker.Shared.Constants;

/// <summary>
/// API route constants shared between client and server
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Base route for repair jobs endpoint
    /// </summary>
    public const string RepairJobs = "api/repairjobs";

    /// <summary>
    /// Route template for updating a repair job's status (format: api/repairjobs/{id}/status)
    /// </summary>
    public const string UpdateStatus = "api/repairjobs/{0}/status";
}
