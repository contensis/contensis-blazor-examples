namespace BlazorWebAssemblyApp.Server.Services;

using BlazorWebAssemblyApp.Shared;
using Newtonsoft.Json;
using Zengenti.Rest.RestClient;

public class UiContentTypeService : IUiContentTypeService
{
    private readonly RestClient _restClient;

    public UiContentTypeService()
    {
        _restClient = new RestClientFactory(Settings.RootUri).SecuredRestClient(Settings.RootUri, Settings.ClientId, Settings.SharedSecret, Settings.Scopes);
    }

    public async Task<List<UiContentType>> GetUiContentTypesFor(string projectApiId)
    {
        try
        {
            var uriString = Path.Combine("/api/management/projects/", projectApiId, "contenttypes");
            var result = await _restClient.GetJsonAsync(uriString);

            var UiContentTypes = JsonConvert.DeserializeObject<List<UiContentType>>(result.Content);

            return UiContentTypes;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public Task<List<UiContentType>> GetUiContentTypesFor(Guid projectUuid)
    {
        return Task.FromResult(new List<UiContentType>());
    }

    public async Task<UiContentType> GetUiContentTypeFor(string projectApiId, string UiContentTypeApiId)
    {
        try
        {
            var uriString = Path.Combine("/api/management/projects/", projectApiId, "contenttypes", UiContentTypeApiId);
            var result = await _restClient.GetJsonAsync(uriString);

            var UiContentTypes = JsonConvert.DeserializeObject<UiContentType>(result.Content);

            return UiContentTypes;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
