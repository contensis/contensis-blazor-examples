using System.Collections.Generic;
using System.Threading.Tasks;
using Zengenti.Contensis.Management;
using Zengenti.Data;

namespace Management.Services
{
    public interface IManagementService
    {
        Task<List<Project>> ListProjectsAsync();

        Task<Project> GetProjectAsync(string apiId);

        Task<PagedList<Entry>> GetEntriesAsync(string projectApiId, string contentTypeApiId, string language = null);
    }

    public class ManagementService : IManagementService
    {
        private readonly ManagementClient _client;

        public ManagementService(ManagementClient client)
        {
            _client = client;
        }

        public Task<List<Project>> ListProjectsAsync()
        {
            return _client.Projects.ListAsync();
        }

        public Task<Project> GetProjectAsync(string apiId)
        {
            return _client.Projects.GetAsync(apiId);
        }

        public async Task<PagedList<Entry>> GetEntriesAsync(string projectApiId, string contentTypeApiId, string language = null)
        {
            var project = await _client.Projects.GetAsync(projectApiId);
            return await project.Entries.ListAsync(contentTypeApiId, language);
        }

        public async Task<Project> CreateProject()
        {
            return _client.Projects.New("projectName", "apiId", "primaryLanguage", new List<string>(), "description");
        }
    }
}