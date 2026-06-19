using RepairStatusTracker.Shared.Enums;

namespace RepairStatusTracker.Shared.Models;

/// <summary>
/// Represents a vehicle repair job
/// </summary>
/// <param name="Id">Unique identifier for the repair job</param>
/// <param name="CustomerName">Name of the customer</param>
/// <param name="VehicleYear">Year of the vehicle</param>
/// <param name="VehicleMake">Make/manufacturer of the vehicle</param>
/// <param name="VehicleModel">Model of the vehicle</param>
/// <param name="RepairCenter">Name of the repair center handling the job</param>
/// <param name="Status">Current status of the repair job</param>
public sealed record RepairJob(
    int Id,
    string CustomerName,
    int VehicleYear,
    string VehicleMake,
    string VehicleModel,
    string RepairCenter,
    RepairStatus Status);