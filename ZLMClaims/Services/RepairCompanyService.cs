using IdentityModel;
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
        private readonly string baseUrl = ApiUrlHelper.GetBaseApiUrl();

        public RepairCompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
        }

        public async Task<IEnumerable<RepairCompany>> GetRepairCompaniesAsync()
        {                   
            string apiUrl = $"{baseUrl}/api/RepairCompanies";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            {
                string content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RepairCompany>>(content);
            }                               
        }
    }
}
