using System.Threading.Tasks;
using BlazorWebAssemblyApp.Client.Services;
using BlazorWebAssemblyApp.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyApp.Client.Pages;

public class ProjectsBase : ComponentBase
{
    protected UiProject[] Projects;

    [Inject]
    public IManagementService ManagementService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Projects = await ManagementService.ListProjects();
    }
}
