using BlazorWebAssemblyApp.Shared;
using System.Text;
using System.Text.Json;
using Zengenti.Data;

namespace BlazorWebAssemblyApp.Client.Services;

public class ManagementService : IManagementService
{
    private readonly HttpClient _httpClient;

    public ManagementService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UiProject[]> ListProjects()
    {
        var projects = await JsonSerializer.DeserializeAsync<IEnumerable<UiProject>>
        (await _httpClient.GetStreamAsync($"managementapi/projects"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return projects
            .ToArray();
    }


    public async Task<UiProject> GetProject(string projectApiId)
    {
        var project = await JsonSerializer.DeserializeAsync<UiProject>
        (await _httpClient.GetStreamAsync($"managementapi/projects/{projectApiId}"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return project;
    }

    public async Task<UiContentType[]> ListContentTypes(string projectApiId)
    {
        var contentTypes = await JsonSerializer.DeserializeAsync<IEnumerable<UiContentType>>
        (await _httpClient.GetStreamAsync($"managementapi/projects/{projectApiId}/contenttypes"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return contentTypes
            .ToArray();
    }


    public async Task<UiEntry[]> ListEntries(
        string projectApiId,
        string contentTypeId = null,
        string language = null,
        PageOptions pageOptions = null,
        IList<string> order = null)
    {
        var entriesUrl = $"managementapi/projects/{projectApiId}/entries";
        if (contentTypeId != null)
        {
            entriesUrl += $"?contentTypeId={contentTypeId}";
        }
        var entries = await JsonSerializer.DeserializeAsync<IEnumerable<UiEntry>>
        (await _httpClient.GetStreamAsync(entriesUrl),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return entries
            .ToArray();
    }

    public async Task<UiEntry> GetEntry(
        string projectApiId,
        string entryId,
        string language = null,
        string version = null)
    {
        var entry = await JsonSerializer.DeserializeAsync<UiEntry>
        (await _httpClient.GetStreamAsync($"managementapi/projects/{projectApiId}/entries/{entryId}"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return entry;
    }

    public async Task<UiEntry> UpdateEntry(string projectApiId, UiEntry entry, string language = null, string version = null)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
        var response = await _httpClient
            .PutAsync($"managementapi/projects/{projectApiId}/entries/{entry.Id}", httpContent);
        var updatedEntry = await JsonSerializer.DeserializeAsync<UiEntry>(
        await response.Content.ReadAsStreamAsync());
        return updatedEntry;
    }

    public async Task<UiContentType> GetContentTypeFor(string projectApiId, string contentTypeApiId)
    {
        var contentType = await JsonSerializer.DeserializeAsync<UiContentType>
        (await _httpClient.GetStreamAsync($"managementapi/projects/{projectApiId}/contenttypes/{contentTypeApiId}"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return contentType;
    }
}
