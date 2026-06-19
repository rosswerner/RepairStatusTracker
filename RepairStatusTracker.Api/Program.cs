using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new[]
{
    new RepairTicket(1, "Sample repair", RepairStatus.Received)
});

app.Run();
