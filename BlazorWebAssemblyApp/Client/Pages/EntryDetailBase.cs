namespace BlazorWebAssemblyApp.Client.Pages;

using System.Threading.Tasks;
using BlazorWebAssemblyApp.Client.Services;
using BlazorWebAssemblyApp.Shared;
using Microsoft.AspNetCore.Components;

public class EntryDetailBase : ComponentBase
{
    [Parameter]
    public string EntryId { get; set; }

    protected UiEntry Entry = new UiEntry();

    [Inject]
    public IManagementService ManagementService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Entry = await ManagementService.GetEntry("website", EntryId);
        //    foreach (var keyValuePair in entry.Fields)
        //    {

        //        Console.WriteLine(keyValuePair.Key + " : " + (keyValuePair.Value == null ? "null" : keyValuePair.Value.ToString()));
        //    }

        //    entry.UpdateField("title", entry.Fields["title"] + "'");
        //    entry.UpdateField("stringData", entry.Fields["stringData"] + "'");
        //    entry.UpdateField("numberData", Convert.ToInt32(entry.Fields["numberData"].ToString()) + 1);
        //    entry.UpdateField("booleanData", !Boolean.Parse(entry.Fields["booleanData"].ToString()));
        //    entry.UpdateField("dateData", DateTime.Parse(entry.Fields["dateData"].ToString()).AddDays(1));
        //    var updatedEntry = await ManagementService.UpdateEntry("website", entry);
        //    if (updatedEntry != null)
        //    {

        //    }

    }
}
