using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Management.Services
{
    public class TokenService
    {
        private readonly HttpClient _httpClient;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetApiKeyTokenFor(string clientId, string sharedSecret)
        {
            var nameValueCollection = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"), 
                new KeyValuePair<string, string>("client_id", clientId), 
                new KeyValuePair<string, string>("client_secret", sharedSecret), 
                new KeyValuePair<string, string>("scope", "Security_Administrator ContentType_Delete ContentType_Read ContentType_Write Entry_Delete Entry_Read Entry_Write Project_Read Project_Write Project_Delete DiagnosticsAllUsers DiagnosticsAdministrator Workflow_Administrator")
            };
            var blah = await _httpClient.PostAsync("authenticate/connect/token",
                new FormUrlEncodedContent(nameValueCollection));

            return blah.Content.ToString();
        }
    }
}