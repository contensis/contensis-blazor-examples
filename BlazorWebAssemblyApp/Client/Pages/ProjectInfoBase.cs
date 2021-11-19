namespace BlazorWebAssemblyApp.Client.Pages;

using System;
using System.Threading.Tasks;
using BlazorWebAssemblyApp.Client.Services;
using BlazorWebAssemblyApp.Shared;
using Microsoft.AspNetCore.Components;

public class ProjectInfoBase : ComponentBase
{
    [Parameter] public string ProjectApiId { get; set; }

    [Inject] public IManagementService ManagementService { get; set; }

    protected string ComponentTitle { get; set; }

    protected string ErrorMessage { get; set; }

    protected UiProject Project;

    protected long EntriesCount;

    protected int RootNodeChildCount;

    protected UiContentType[] ContentTypes;

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrWhiteSpace(ProjectApiId))
        {
            ComponentTitle = "No project specified";
            ErrorMessage = "Specify a project api id";
            return;
        }

        try
        {
            Project = await ManagementService.GetProject(ProjectApiId);

            ComponentTitle = "Project Info:";

            // var entriesList = await Project.Entries.ListAsync();
            // EntriesCount = entriesList.TotalCount;
            //
            // var rootNode = await Project.Nodes.GetRootAsync();
            // RootNodeChildCount = rootNode.ChildCount;

            ContentTypes = await ManagementService.ListContentTypes(Project.Id);
        }
        catch (Exception e)
        {
            ComponentTitle = "Unable to retrieve project";
            ErrorMessage = $"Unable to get project with api id of '{ProjectApiId}'";
        }
    }
}
