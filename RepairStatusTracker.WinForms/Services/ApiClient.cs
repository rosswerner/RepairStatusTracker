using System.Net.Http.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using RepairStatusTracker.Shared.Constants;
using RepairStatusTracker.Shared.Models;

namespace RepairStatusTracker.WinForms.Services;

/// <summary>
/// Client for interacting with the Repair Status Tracker API
/// </summary>
public sealed class ApiClient
{
    private readonly HttpClient httpClient;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    /// <summary>
    /// Initializes a new instance of the ApiClient
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API requests</param>
    public ApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <summary>
    /// Gets all repair jobs from the API
    /// </summary>
    /// <returns>A read-only list of repair jobs</returns>
    public async Task<IReadOnlyList<RepairJob>> GetRepairJobsAsync()
    {
        var jobs = await httpClient.GetFromJsonAsync<List<RepairJob>>(ApiRoutes.RepairJobs, JsonOptions);
        return jobs ?? [];
    }

    /// <summary>
    /// Updates the status of a repair job
    /// </summary>
    /// <param name="jobId">The ID of the job to update</param>
    /// <param name="status">The new status value</param>
    /// <returns>True if the update was successful; false if the job was not found</returns>
    /// <exception cref="HttpRequestException">Thrown when the API returns an unexpected status code</exception>
    public async Task<bool> UpdateStatusAsync(int jobId, string status)
    {
        var route = string.Format(ApiRoutes.UpdateStatus, jobId);
        var response = await httpClient.PatchAsJsonAsync(route, new { status }, JsonOptions);

        return response.StatusCode switch
        {
            System.Net.HttpStatusCode.NoContent => true,
            System.Net.HttpStatusCode.NotFound => false,
            _ => throw new HttpRequestException(
                $"Unexpected status code: {response.StatusCode}")
        };
    }
}