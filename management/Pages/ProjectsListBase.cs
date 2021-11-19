using System.Collections.Generic;
using System.Threading.Tasks;
using Management.Services;
using Microsoft.AspNetCore.Components;
using Zengenti.Contensis.Management;

namespace Management.Pages
{
    public class ProjectsListBase : ComponentBase
    {
        [Inject] public IManagementService ManagementService { get; set; }

        protected IEnumerable<Project> Projects;

        protected override async Task OnInitializedAsync()
        {
            Projects = await ManagementService.ListProjectsAsync();
        }
    }
}