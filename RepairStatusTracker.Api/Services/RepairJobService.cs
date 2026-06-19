using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;

namespace RepairStatusTracker.Api.Services;

/// <summary>
/// Service for managing repair jobs
/// </summary>
public sealed class RepairJobService
{
    private readonly List<RepairJob> _jobs;

    /// <summary>
    /// Initializes a new instance of the RepairJobService with seed data
    /// </summary>
    public RepairJobService()
    {
        _jobs = new List<RepairJob>(Data.RepairJobSeedData.Jobs);
    }

    /// <summary>
    /// Gets all repair jobs
    /// </summary>
    /// <returns>A read-only list of all repair jobs</returns>
    public IReadOnlyList<RepairJob> GetAllJobs()
    {
        return _jobs.AsReadOnly();
    }

    /// <summary>
    /// Updates the status of a repair job
    /// </summary>
    /// <param name="jobId">The ID of the job to update</param>
    /// <param name="newStatus">The new status to set</param>
    /// <returns>True if the job was found and updated; otherwise false</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the new status is not a valid RepairStatus value</exception>
    public bool UpdateStatus(int jobId, RepairStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(RepairStatus), newStatus))
        {
            throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, "Invalid repair status value.");
        }

        var jobIndex = _jobs.FindIndex(job => job.Id == jobId);
        if (jobIndex < 0)
        {
            return false;
        }

        _jobs[jobIndex] = _jobs[jobIndex] with { Status = newStatus };
        return true;
    }
}
