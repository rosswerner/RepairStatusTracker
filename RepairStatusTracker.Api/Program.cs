using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Services;
using RepairStatusTracker.Shared.Models;
using RepairStatusTracker.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RepairJobService>();

var app = builder.Build();

app.MapGet("/", () => new[]
{
    new RepairTicket(1, "Sample repair", RepairStatus.Received)
});

app.MapGet("/api/repairjobs", (RepairJobService repairJobService) => repairJobService.GetAllJobs());

app.MapPatch("/api/repairjobs/{id:int}/status", (
    int id,
    RepairJobStatusUpdateRequest request,
    RepairJobService repairJobService) =>
{
    if (string.IsNullOrWhiteSpace(request.Status) ||
        !Enum.TryParse<RepairStatus>(request.Status, ignoreCase: true, out var newStatus) ||
        !Enum.IsDefined(typeof(RepairStatus), newStatus))
    {
        return Results.BadRequest(new { error = "Invalid repair status." });
    }

    var updated = repairJobService.UpdateStatus(id, newStatus);
    return updated ? Results.NoContent() : Results.NotFound();
});

app.Run();
