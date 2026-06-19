using System.Net.Http.Json;
using RepairStatusTracker.Shared.Models;

namespace RepairStatusTracker.WinForms.Services;

public sealed class ApiClient
{
    private readonly HttpClient httpClient;

    public ApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IReadOnlyList<RepairJob>> GetRepairJobsAsync()
    {
        var jobs = await httpClient.GetFromJsonAsync<List<RepairJob>>("api/repairjobs");
        return jobs ?? [];
    }

    public async Task<bool> UpdateStatusAsync(int jobId, string status)
    {
        var response = await httpClient.PatchAsJsonAsync($"api/repairjobs/{jobId}/status", new { status });

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();
        return false;
    }
}