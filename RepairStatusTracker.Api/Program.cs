using System.Text.Json.Serialization;
using System.Text.Json.Serialization;
using RepairStatusTracker.Api.Dtos;
using RepairStatusTracker.Api.Services;
using RepairStatusTracker.Shared.Constants;
using RepairStatusTracker.Shared.Enums;
using RepairStatusTracker.Shared.Models;
using RepairStatusTracker.Shared.Validation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RepairJobService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Repair Status Tracker API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Repair Status Tracker API v1");
        options.RoutePrefix = "swagger";
    });
}

app.MapGet("/", () => Results.Redirect("/swagger"))
.WithName("GetRoot")
.WithTags("General")
.WithSummary("Redirect to API documentation")
.WithDescription("Redirects to the Swagger UI documentation page.")
.ExcludeFromDescription();

app.MapGet(ApiRoutes.RepairJobs, (RepairJobService repairJobService) => repairJobService.GetAllJobs())
.WithName("GetAllRepairJobs")
.WithTags("Repair Jobs")
.WithSummary("Get all repair jobs")
.WithDescription("Retrieves a list of all repair jobs with their current status.")
.Produces<IReadOnlyList<RepairJob>>(StatusCodes.Status200OK);

app.MapPatch(string.Format(ApiRoutes.UpdateStatus, "{id:int}"), (
    int id,
    RepairJobStatusUpdateRequest request,
    RepairJobService repairJobService) =>
{
    if (!RepairStatusValidator.IsValid(request.Status, out var newStatus))
    {
        return Results.BadRequest(new { error = "Invalid repair status." });
    }

    var updated = repairJobService.UpdateStatus(id, newStatus);
    return updated ? Results.NoContent() : Results.NotFound();
})
.WithName("UpdateRepairJobStatus")
.WithTags("Repair Jobs")
.WithSummary("Update repair job status")
.WithDescription("Updates the status of a specific repair job. Valid status values are: Received, InProgress, WaitingOnParts, QualityCheck, ReadyForPickup, Completed.")
.Accepts<RepairJobStatusUpdateRequest>("application/json")
.Produces(StatusCodes.Status204NoContent)
.Produces<object>(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound);

app.Run();
