using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;

namespace RepairStatusTracker.Shared.Services;

public sealed class RepairJobService
{
    private readonly List<RepairJob> _jobs;

    public RepairJobService()
    {
        _jobs = new List<RepairJob>(RepairJobMockData.Jobs);
    }

    public IReadOnlyList<RepairJob> GetAllJobs()
    {
        return _jobs.AsReadOnly();
    }

    public bool UpdateStatus(int jobId, RepairStatus newStatus)
    {
        var jobIndex = _jobs.FindIndex(job => job.Id == jobId);
        if (jobIndex < 0)
        {
            return false;
        }

        _jobs[jobIndex] = _jobs[jobIndex] with { Status = newStatus };
        return true;
    }
}