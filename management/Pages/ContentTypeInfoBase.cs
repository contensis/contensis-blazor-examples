using System;
using System.Threading.Tasks;
using Management.Model;
using Management.Services;
using Microsoft.AspNetCore.Components;
using Zengenti.Contensis.Management;
using Zengenti.Data;

namespace Management.Pages
{
    public class ContentTypeInfoBase : ComponentBase
    {
        [Parameter] public string ProjectApiId { get; set; }

        [Parameter] public string ContentTypeApiId { get; set; }

        [Inject] public IManagementService ManagementService { get; set; }

        [Inject] public IContentTypeService ContentTypeService { get; set; }

        protected ContentType ContentType;

        protected PagedList<Entry> Entries;
        
        protected string ComponentTitle { get; set; }

        protected string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ComponentTitle = ContentTypeApiId;

                ContentType = await ContentTypeService.GetContentTypeFor(ProjectApiId, ContentTypeApiId);

                Entries =
                    await ManagementService.GetEntriesAsync(ProjectApiId, ContentTypeApiId, ContentType.DefaultLanguage);

            }
            catch (Exception e)
            {
                ComponentTitle = "Unable to retrieve content type";
                ErrorMessage = $"Unable to get content type with api id of '{ContentTypeApiId}'";
            }
        }
    }
}