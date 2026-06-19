using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;

namespace RepairStatusTracker.Api.Data;

/// <summary>
/// Mock data for seeding repair jobs
/// </summary>
public static class RepairJobSeedData
{
    public static readonly IReadOnlyList<RepairJob> Jobs = new List<RepairJob>
    {
        new(1, "Alicia Morgan", 2022, "Honda", "CR-V", "Northside Collision Center", RepairStatus.Received),
        new(2, "Benjamin Torres", 2021, "Toyota", "Camry", "Precision Auto Body", RepairStatus.InProgress),
        new(3, "Chloe Bennett", 2019, "Ford", "F-150", "East River Collision", RepairStatus.WaitingOnParts),
        new(4, "Daniel Kim", 2023, "Tesla", "Model Y", "Premier Repair Works", RepairStatus.QualityCheck),
        new(5, "Emma Patel", 2018, "Chevrolet", "Equinox", "Metro Collision Group", RepairStatus.ReadyForPickup),
        new(6, "Franklin Reed", 2020, "Nissan", "Altima", "Northside Collision Center", RepairStatus.Completed),
        new(7, "Grace Thompson", 2024, "Subaru", "Outback", "Summit Auto Repair", RepairStatus.Received),
        new(8, "Hector Alvarez", 2017, "Jeep", "Grand Cherokee", "Precision Auto Body", RepairStatus.InProgress),
        new(9, "Isabella Nguyen", 2022, "BMW", "X3", "Harbor Collision & Paint", RepairStatus.WaitingOnParts),
        new(10, "Jordan Ellis", 2016, "Kia", "Sorento", "East River Collision", RepairStatus.QualityCheck),
        new(11, "Katherine Brooks", 2021, "Hyundai", "Tucson", "Premier Repair Works", RepairStatus.ReadyForPickup),
        new(12, "Liam Walker", 2019, "Volkswagen", "Passat", "Metro Collision Group", RepairStatus.Completed),
        new(13, "Maya Hernandez", 2023, "Mazda", "CX-5", "Summit Auto Repair", RepairStatus.Received),
        new(14, "Noah Stewart", 2020, "Ram", "1500", "Harbor Collision & Paint", RepairStatus.InProgress),
        new(15, "Olivia Price", 2018, "Lexus", "RX 350", "Northside Collision Center", RepairStatus.ReadyForPickup)
    };
}
