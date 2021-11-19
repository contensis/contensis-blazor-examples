namespace BlazorWebAssemblyApp.Server.Controllers;

using BlazorWebAssemblyApp.Server.Services;
using BlazorWebAssemblyApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Zengenti.Contensis.Management;
using Zengenti.Data;

[ApiController]
[Route("[controller]")]
public class ManagementApiController : ControllerBase
{
    private readonly ILogger<ManagementApiController> logger;
    private readonly ManagementClient _client;
    private readonly IUiContentTypeService _contentTypeService;

    public ManagementApiController
    (
        ILogger<ManagementApiController> logger,
        ManagementClient client,
        IUiContentTypeService contentTypeService
    )
    {
        this.logger = logger;
        _client = client;
        _contentTypeService = contentTypeService;
    }

    [HttpGet("projects")]
    public async Task<UiProject[]> ListProjects()
    {
        var listProjects = await _client.Projects.ListAsync();
        return listProjects
            .Select(MapToUiProject)
            .ToArray();
    }

    [HttpGet("projects/{projectApiId}")]
    public async Task<UiProject> GetProject(string projectApiId)
    {
        var project = await _client.Projects.GetAsync(projectApiId);
        return MapToUiProject(project);
    }

    [HttpGet("projects/{projectApiId}/contenttypes")]
    public async Task<UiContentType[]> ListContentTypes(string projectApiId)
    {
        var result = await _contentTypeService.GetUiContentTypesFor(projectApiId);
        return result.ToArray();
    }

    [HttpGet("projects/{projectApiId}/contenttypes/{contentTypeId}")]
    public async Task<UiContentType> GetContentType(string projectApiId, string contentTypeId)
    {
        var result = await _contentTypeService.GetUiContentTypeFor(projectApiId, contentTypeId);
        return result;
    }

    [HttpGet("projects/{projectApiId}/entries")]
    public async Task<UiEntry[]> ListEntries(
        [FromRoute] string projectApiId,
        [FromQuery] string? contentTypeId = null,
        [FromQuery] string? language = "en-GB",
        [FromQuery] int? pageIndex = 0,
        [FromQuery] int? pageSize = 25,
        [FromQuery] IList<string>? order = null)
    {
        var project = await _client.Projects.GetAsync(projectApiId);
        Console.WriteLine($"language: {language}");
        var entriesList = await project.Entries.ListAsync(contentTypeId, language, new PageOptions(pageIndex ?? 0, pageSize ?? 25), order);
        return entriesList.Items
            .Select(MapToUiEntry)
            .ToArray();
    }

    [HttpGet("projects/{projectApiId}/entries/{entryId}")]
    public async Task<UiEntry> GetEntry(
        [FromRoute] string projectApiId,
        [FromRoute] string entryId,
        [FromQuery] string? language = null,
        [FromQuery] string? version = null)
    {
        var project = await _client.Projects.GetAsync(projectApiId);
        var entry = await project.Entries.GetAsync(entryId, language, version);
        return MapToUiEntry(entry);
    }

    [HttpPut("projects/{projectApiId}/entries/{entryId}")]
    public async Task<UiEntry> UpdateEntry(
        [FromRoute] string projectApiId,
        [FromRoute] string entryId,
        [FromBody] UiEntry entry,
        [FromQuery] string? language = null,
        [FromQuery] string? version = null)
    {
        var uiEntry = entry;
        var project = await _client.Projects.GetAsync(projectApiId);
        var apiEntry = await project.Entries.GetAsync(entryId, language, version);
        foreach (var fieldName in apiEntry.FieldNames)
        {
            if (uiEntry.UpdatedFields == null || uiEntry.UpdatedFields.Contains(fieldName))
            {
                apiEntry.Set(fieldName, uiEntry.Fields[fieldName]);
            }
        }

        await apiEntry.SaveAsync();
        return MapToUiEntry(apiEntry);
    }

    private UiProject MapToUiProject(Project project)
    {
        return new UiProject()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }

    private UiEntry MapToUiEntry(Entry entry)
    {
        var uiEntry = new UiEntry()
        {
            Id = entry.Id,
            Slug = entry.Slug,
            Version = entry.Version.VersionNo
        };

        foreach (var fieldName in entry.FieldNames)
        {
            uiEntry.Fields.Add(fieldName, entry.Get<object>(fieldName));
        }

        return uiEntry;
    }
}
