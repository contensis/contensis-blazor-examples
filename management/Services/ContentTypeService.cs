using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zengenti.Rest.RestClient;
using ContentType = Management.Model.ContentType;

namespace Management.Services
{
    public interface IContentTypeService
    {
        Task<List<ContentType>> GetContentTypesFor(string projectApiId);
        Task<List<ContentType>> GetContentTypesFor(Guid projectUuid);
        Task<ContentType> GetContentTypeFor(string projectApiId, string contentTypeApiId);
    }

    public class ContentTypeService : IContentTypeService
    {
        private readonly RestClient _restClient;

        public ContentTypeService()
        {
            _restClient = new RestClientFactory(Settings.RootUri).SecuredRestClient(Settings.RootUri, Settings.ClientId, Settings.SharedSecret, Settings.Scopes);
        }

        public async Task<List<ContentType>> GetContentTypesFor(string projectApiId)
        {
            try
            {
                var uriString = Path.Combine("/api/management/projects/", projectApiId, "contenttypes");
                var result = await _restClient.GetJsonAsync(uriString);

                var contentTypes = JsonConvert.DeserializeObject<List<ContentType>>(result.Content);

                return contentTypes;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Task<List<ContentType>> GetContentTypesFor(Guid projectUuid)
        {
            return Task.FromResult(new List<ContentType>());
        }

        public async Task<ContentType> GetContentTypeFor(string projectApiId, string contentTypeApiId)
        {
            try
            {
                var uriString = Path.Combine("/api/management/projects/", projectApiId, "contenttypes", contentTypeApiId);
                var result = await _restClient.GetJsonAsync(uriString);

                var contentTypes = JsonConvert.DeserializeObject<ContentType>(result.Content);

                return contentTypes;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

}