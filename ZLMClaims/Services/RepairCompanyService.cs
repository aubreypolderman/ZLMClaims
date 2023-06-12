using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Auth0;
using ZLMClaims.Models;

namespace ZLMClaims.Services
{
    public class RepairCompanyService : IRepairCompanyService
    {
        private readonly HttpClient _httpClient;

        public RepairCompanyService(HttpClient httpClient)
        {
#if DEBUG
            _httpClient = httpClient; 
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); 

            // new use TokenHandler.AccessToken
            _httpClient.DefaultRequestHeaders.Authorization
             = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);

#else
            _httpClient = new HttpClient();
#endif
        }

        public async Task<IEnumerable<RepairCompany>> GetRepairCompaniesAsync()
        {                   
            string ApiUrl = "https://10.0.2.2:7040/api/RepairCompanies";
            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
            {
                string content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RepairCompany>>(content);
            }                               
        }

        public async Task<IEnumerable<RepairCompany>> GetRepairCompaniesAsyncOld()
        {
            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/RepairCompanies");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RepairCompany>>(responseContent);
            }
            else
            {
                throw new HttpRequestException($"Error getting repaircompanies: {response.ReasonPhrase}");
            }
        }

        public async Task<RepairCompany> GetRepairCompanyByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            { 
                return System.Text.Json.JsonSerializer.Deserialize<RepairCompany>(responseContent); 
            }
            else
            {
                throw new HttpRequestException($"Error getting repair company with id {id}: {response.ReasonPhrase}");
            }
        }
    }
}
