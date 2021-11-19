using System;
using System.Threading.Tasks;
using Management.Services;
using Microsoft.AspNetCore.Components;
using Zengenti.Contensis.Management;

namespace Management.Pages
{
    public class ProjectEditBase : ComponentBase
    {
        [Parameter] public string ProjectId { get; set; }

        [Inject] public IManagementService ManagementService { get; set; }

        protected string ComponentTitle { get; set; }

        protected string ErrorMessage { get; set; }

        protected Project Project;

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                ComponentTitle = "Create new project:";
                return;
            }

            try
            {
                Project = await ManagementService.GetProjectAsync(ProjectId);
                ComponentTitle = $"Edit project '{Project.Name}':";
            }
            catch (Exception e)
            {
                ComponentTitle = "Create new project:";
                ErrorMessage = $"Unable to get project with api id of '{ProjectId}'";
            }
        }
    }
}