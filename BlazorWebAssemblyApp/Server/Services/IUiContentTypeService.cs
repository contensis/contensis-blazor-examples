namespace BlazorWebAssemblyApp.Server.Services;

using BlazorWebAssemblyApp.Shared;

public interface IUiContentTypeService
{
    Task<List<UiContentType>> GetUiContentTypesFor(string projectApiId);
    Task<List<UiContentType>> GetUiContentTypesFor(Guid projectUuid);
    Task<UiContentType> GetUiContentTypeFor(string projectApiId, string UiContentTypeApiId);
}
