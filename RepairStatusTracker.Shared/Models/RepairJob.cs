using RepairStatusTracker.Shared.Enums;

namespace RepairStatusTracker.Shared.Models;

public sealed record RepairJob(
    int Id,
    string CustomerName,
    int VehicleYear,
    string VehicleMake,
    string VehicleModel,
    string RepairCenter,
    RepairStatus Status);