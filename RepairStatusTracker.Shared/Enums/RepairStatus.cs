namespace RepairStatusTracker.Shared.Enums;

/// <summary>
/// Represents the status of a repair job
/// </summary>
public enum RepairStatus
{
    /// <summary>
    /// Job has been received and is awaiting processing
    /// </summary>
    Received,

    /// <summary>
    /// Job is currently being worked on
    /// </summary>
    InProgress,

    /// <summary>
    /// Job is on hold waiting for parts to arrive
    /// </summary>
    WaitingOnParts,

    /// <summary>
    /// Job is undergoing quality inspection
    /// </summary>
    QualityCheck,

    /// <summary>
    /// Job is complete and ready for customer pickup
    /// </summary>
    ReadyForPickup,

    /// <summary>
    /// Job has been completed and picked up by customer
    /// </summary>
    Completed
}