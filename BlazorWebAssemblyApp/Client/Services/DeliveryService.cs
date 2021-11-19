using BlazorWebAssemblyApp.Shared;
using System.Text.Json;
using Zengenti.Data;

namespace BlazorWebAssemblyApp.Client.Services;

public class DeliveryService : IDeliveryService
{

    private readonly HttpClient _httpClient;

    public DeliveryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UiEntry[]> ListEntries(
        string projectApiId,
        string contentTypeId = null,
        string language = null,
        PageOptions pageOptions = null,
        IList<string> order = null)
    {
        var entriesUrl = $"deliveryapi/projects/{projectApiId}/entries";
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
}
