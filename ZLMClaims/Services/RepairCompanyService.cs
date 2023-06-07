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
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [Constructor] DEBUG with TokenHolder.AccessToken = " + TokenHolder.AccessToken);
            _httpClient = httpClient; //👈 new code
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field

            // new use TokenHandler.AccessToken
            _httpClient.DefaultRequestHeaders.Authorization
             = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [Constructor] TokenHolder.AccessToken = " + TokenHolder.AccessToken);

#else
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [Constructor] ELSE with TokenHolder.AccessToken = " + TokenHolder.AccessToken);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [Constructor] ELSE " + _httpClient.DefaultRequestHeaders.Authorization);
            _httpClient = new HttpClient();
#endif
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [Constructor] End ");
        }

        public async Task<IEnumerable<RepairCompany>> GetRepairCompaniesAsync()
        {
                    
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] Get all repaircompanies ");
            // var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/RepairCompanies");            
            string ApiUrl = "https://10.0.2.2:7040/api/RepairCompanies";

            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] AccessToken: " + TokenHolder.AccessToken);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] httpClient.DefaultRequestHeaders.Authorization: " + _httpClient.DefaultRequestHeaders.Authorization);
            //try
            //{
                Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync]");
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] Return content: " + content);
                    return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RepairCompany>>(content);
                }                
                
            //}
            //catch (Exception ex)
            //{
            //}
        }

        public async Task<IEnumerable<RepairCompany>> GetRepairCompaniesAsyncOld()
        {

            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] Get all repaircompanies ");

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/RepairCompanies");
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] StatusCode response: " + response.IsSuccessStatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] ReasonPhrase response: " + response.ReasonPhrase); ;
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                // responseContent = LoadData();  
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RepairCompany>>(responseContent);
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] Error getting repaircompanies");
                throw new HttpRequestException($"Error getting repaircompanies: {response.ReasonPhrase}");
            }
        }

        public async Task<RepairCompany> GetRepairCompanyByIdAsync(int id)
        {
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] Get repair company with id " + id);
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
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

        private static string LoadData()
        {
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [LoadDataAsync ][==============] ");

            return @"[
                {
                    ""id"": 1,
                    ""name"": ""Renova"",
                    ""email"": ""rcc@renova.nl"",
                    ""street"": ""Amundsenweg"",
                    ""housenumber"": ""33"",
                    ""city"": ""Goes"",
                    ""zipcode"": ""4462 GP"",
                    ""latitude"": 51.951604134000284,
                    ""longitude"": 4.433766624425165,
                    ""phone"": ""0113 - 2454 225"",
                    ""website"": ""www.renova.nl""               
                },
 {
                    ""id"": 2,
                    ""name"": ""Van den Berg Autoschade"",
                    ""email"": ""info@vandenbergautoschade.nl"",
                    ""street"": ""Hermesweg"",
                    ""housenumber"": ""5"",
                    ""city"": ""Vlissingen"",
                    ""zipcode"": ""4382 ND"",
                    ""latitude"": 51.896881492621496,
                    ""longitude"": 4.504703826310183,
                    ""phone"": ""0118 - 414 329"",
                    ""website"": ""www.vandenbergautoschade.nl""
                }
            ]";
        }
    }
}
