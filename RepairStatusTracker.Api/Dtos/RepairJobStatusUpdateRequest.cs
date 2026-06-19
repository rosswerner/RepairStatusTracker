using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace RepairStatusTracker.Api.Dtos;

/// <summary>
/// Request to update a repair job's status
/// </summary>
/// <param name="Status">The new status for the repair job. Valid values: Received, InProgress, WaitingOnParts, QualityCheck, ReadyForPickup, Completed</param>
public sealed record RepairJobStatusUpdateRequest(
    [property: Required]
    string? Status);