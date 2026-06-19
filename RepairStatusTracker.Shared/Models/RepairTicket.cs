using RepairStatusTracker.Shared.Enums;

namespace RepairStatusTracker.Shared.Models;

public sealed record RepairTicket(int Id, string Title, RepairStatus Status);