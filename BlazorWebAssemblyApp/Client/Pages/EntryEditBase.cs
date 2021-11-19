namespace BlazorWebAssemblyApp.Client.Pages;

using System;
using System.Threading.Tasks;
using BlazorWebAssemblyApp.Client.Services;
using BlazorWebAssemblyApp.Shared;
using Microsoft.AspNetCore.Components;

public class EntryEditBase : ComponentBase
{
    [Parameter]
    public string EntryId { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IManagementService ManagementService { get; set; }

    protected UiEntry Entry = new UiEntry();

    protected string Title;
    protected string StringData;
    protected int NumberData;
    protected bool BooleanData;
    protected DateTime DateData;

    protected string Message = string.Empty;
    protected string StatusClass = string.Empty;
    protected bool Saved;

    protected override async Task OnInitializedAsync()
    {
        Saved = false;

        Entry = await ManagementService.GetEntry("website", EntryId);
        Title = Entry.Fields["title"].ToString();
        StringData = Entry.Fields["stringData"].ToString();
        NumberData = Convert.ToInt32(Entry.Fields["numberData"].ToString());
        BooleanData = Boolean.Parse(Entry.Fields["booleanData"].ToString());
        DateData = DateTime.Parse(Entry.Fields["dateData"].ToString());
    }

    protected async Task HandleValidSubmit()
    {
        Entry.UpdateField("title", Title);
        Entry.UpdateField("stringData", StringData);
        Entry.UpdateField("numberData", NumberData);
        Entry.UpdateField("booleanData", BooleanData);
        Entry.UpdateField("dateData", DateData);
        var updatedEntry = await ManagementService.UpdateEntry("website", Entry);
        if (updatedEntry != null)
        {
            StatusClass = "alert-success";
            Message = "Entry updated successfully.";
            Saved = true;
        }
    }

    protected void NavigateToEntries()
    {
        NavigationManager.NavigateTo("/entries");
    }
}
