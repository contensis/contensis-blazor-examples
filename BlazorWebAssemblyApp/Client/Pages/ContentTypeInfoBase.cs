namespace BlazorWebAssemblyApp.Client.Pages;

using System;
using System.Threading.Tasks;
using BlazorWebAssemblyApp.Client.Services;
using BlazorWebAssemblyApp.Shared;
using Microsoft.AspNetCore.Components;
using Zengenti.Data;

public class ContentTypeInfoBase : ComponentBase
{
    [Parameter] public string ProjectApiId { get; set; }

    [Parameter] public string ContentTypeApiId { get; set; }

    [Inject] public IManagementService ManagementService { get; set; }

    protected UiContentType ContentType;

    protected UiEntry[] Entries;

    protected string ComponentTitle { get; set; }

    protected string ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ComponentTitle = ContentTypeApiId;

            ContentType = await ManagementService.GetContentTypeFor(ProjectApiId, ContentTypeApiId);

            Entries =
                await ManagementService.ListEntries(ProjectApiId, ContentTypeApiId, ContentType.DefaultLanguage);

        }
        catch (Exception e)
        {
            ComponentTitle = "Unable to retrieve content type";
            ErrorMessage = $"Unable to get content type with api id of '{ContentTypeApiId}'";
        }
    }
}
