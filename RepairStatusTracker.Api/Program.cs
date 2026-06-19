using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Services;
using RepairStatusTracker.Shared.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RepairJobService>();

var app = builder.Build();

app.MapGet("/", () => new[]
{
    new RepairTicket(1, "Sample repair", RepairStatus.Received)
});

app.MapGet("/api/repairjobs", (RepairJobService repairJobService) => repairJobService.GetAllJobs());

app.Run();
