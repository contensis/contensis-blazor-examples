namespace BlazorWebAssemblyApp.Server.Controllers;

using BlazorWebAssemblyApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Zengenti.Contensis.Delivery;

[ApiController]
[Route("[controller]")]
public class DeliveryApiController : ControllerBase
{
    private readonly ILogger<DeliveryApiController> logger;
    private readonly ContensisClient _client;

    public DeliveryApiController(
       ILogger<DeliveryApiController> logger,
       ContensisClient client)
    {
        this.logger = logger;
        _client = client;
    }

    public Task<Zengenti.Contensis.Delivery.Node> GetNodes()
    {
        var node = _client.Nodes.GetRoot();
        return Task.FromResult(node);
    }

    [HttpGet("projects/{projectApiId}/entries")]
    public async Task<UiEntry[]> GetEntries()
    {
        var entries = await _client.Entries.ListAsync("book", null);


        return entries.Items
            .Select(MapToUiEntry).ToArray();

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
        };

        foreach (var fieldName in entry.FieldNames)
        {
            if (fieldName == "entryTitle" || fieldName == "publishDate")
            {
                uiEntry.Fields.Add(fieldName, entry.Get<object>(fieldName));
            }

            if (fieldName == "coverPhoto")
            {
                var photo = entry.Get<object>(fieldName);
                var id = ((Zengenti.Contensis.Delivery.Image)photo).Asset.Id.ToString();

                uiEntry.Fields.Add(fieldName, id);
            }


        }

        return uiEntry;
    }
}
