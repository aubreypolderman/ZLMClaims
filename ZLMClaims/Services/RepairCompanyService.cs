using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Models;

namespace ZLMClaims.Services
{
    public class RepairCompanyService : IRepairCompanyService
    {
        private readonly HttpClient _httpClient;

        public RepairCompanyService(HttpClient httpClient)
        {
            // check to see if _httpClient instance is not null            
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [constructor] httpclient injected ");
            //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
#if DEBUG
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [constructor] httpclient injected => it's DEBUG so use handler ");    
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
                        _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field
#else                        
                        Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [constructor] httpclient injected => it's NOT DEBUG so instantiate new HttpClient ");
                        _httpClient = new HttpClient();
#endif
        }

        public async Task<IEnumerable<RepairCompany>> GetRepairCompaniesAsync()
        {
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync]");

            // online 
            /*
            HttpResponseMessage response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode();
            Console.WriteLine("[..............] [RepairCompanyService] [GetRepairCompaniesAsync] statuscode: " + response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("[..............] [RepairCompanyService] [GetRepairCompaniesAsync] reponse json: " + json);
            */

            // offline
            // for testpurpose only
            // var json = LoadData();            
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] Get all repaircompanies ");
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] voor de call");

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/RepairCompanies");            
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] reponsecontent: " + responseContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] StatuscodeSucces is all good! Return response");
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RepairCompany>>(responseContent);
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] Errortje getting repaircompanies");
                throw new HttpRequestException($"Error getting repaircompanies: {response.ReasonPhrase}");
            }

            Console.WriteLine( DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompaniesAsync] reponse json: " + responseContent);

            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RepairCompany>>(responseContent);
        }

        public async Task<RepairCompany> GetRepairCompanyByIdAsync(int id)
        {
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] Get repair company with id " + id);
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] reponsecontent: " + responseContent);

            if (response.IsSuccessStatusCode)
            { 
                Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] StatuscodeSucces is all good! Return response");
                return System.Text.Json.JsonSerializer.Deserialize<RepairCompany>(responseContent); 
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [RepairCompanyService] [GetRepairCompanyByIdAsync] Errortje getting repair company with id {id} ");
                throw new HttpRequestException($"Error getting repair company with id {id}: {response.ReasonPhrase}");
            }
        }

        private static string LoadData()
        {
            Console.WriteLine(DateTime.Now + "[RepairCompanyService] [LoadDataAsync ][==============] ");

            return @"[
                {
                    ""id"": 1,
                    ""name"": ""Renova"",
                    ""username"": ""Renova"",
                    ""email"": ""rcc@renova.nl"",
                    ""address"": {
                        ""street"": ""Amundsenweg"",
                        ""housenumber"": ""33"",
                        ""city"": ""Goes"",
                        ""zipcode"": ""4462 GP"",
                        ""geo"": {
                            ""lat"": ""-37.3159"",
                            ""lng"": ""81.1496""
                        }
                    },
                    ""phone"": ""0113 - 2454 225"",
                    ""website"": ""www.renova.nl"",
                    ""company"": {
                        ""name"": ""Renova"",
                        ""catchPhrase"": ""Merk schadeherstelbedrijf (MINI)"",
                        ""bs"": ""harness real-time e-markets""
                    }
                },
 {
                    ""id"": 2,
                    ""name"": ""Van den Berg Autoschade"",
                    ""username"": ""Van den Berg"",
                    ""email"": ""info@vandenbergautoschade.nl"",
                    ""address"": {
                        ""street"": ""Hermesweg"",
                        ""housenumber"": ""5"",
                        ""city"": ""Vlissingen"",
                        ""zipcode"": ""4382 ND"",
                        ""geo"": {
                            ""lat"": ""-43.9509"",
                            ""lng"": ""-34.4618""
                        }
                    },
                    ""phone"": ""0118 - 414 329"",
                    ""website"": ""www.vandenbergautoschade.nl"",
                    ""company"": {
                        ""name"": ""Van den Berg Autoschade"",
                        ""catchPhrase"": ""Universeel schaddeherstelbedrijf"",
                        ""bs"": ""synergize scalable supply-chains""
                    }
                }
            ]";
        }
    }
}
