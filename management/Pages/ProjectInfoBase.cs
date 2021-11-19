using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.Model;
using Management.Services;
using Microsoft.AspNetCore.Components;
using Zengenti.Contensis.Management;

namespace Management.Pages
{
    public class ProjectInfoBase : ComponentBase
    {
        [Parameter] public string ProjectApiId { get; set; }

        [Inject] public IManagementService ManagementService { get; set; }

        [Inject] public IContentTypeService ContentTypeService { get; set; }

        protected string ComponentTitle { get; set; }

        protected string ErrorMessage { get; set; }

        protected Project Project;

        protected long EntriesCount;

        protected int RootNodeChildCount;

        protected List<ContentType> ContentTypes;

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
                Project = await ManagementService.GetProjectAsync(ProjectApiId);
                
                ComponentTitle = "Project Info:";
                
                var entriesList = await Project.Entries.ListAsync();
                EntriesCount = entriesList.TotalCount;

                var rootNode = await Project.Nodes.GetRootAsync();
                RootNodeChildCount = rootNode.ChildCount;

                ContentTypes = await ContentTypeService.GetContentTypesFor(Project.Id);
            }
            catch (Exception e)
            {
                ComponentTitle = "Unable to retrieve project";
                ErrorMessage = $"Unable to get project with api id of '{ProjectApiId}'";
            }
        }
    }
}