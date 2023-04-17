using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Models;

namespace ZLMClaims.Services
{
    public class ContractService : IContractService
    {
        private readonly HttpClient _httpClient;

        public ContractService(HttpClient httpClient)
        {
            // check to see if _httpClient instance is not null    
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Contract>> GetContractsAsync()
        {
            Console.WriteLine("[..............] [ContractService] [GetContractsAsync]");
            HttpResponseMessage response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode();
            Console.WriteLine("[..............] [ContractService] [GetContractsAsync] statuscode: " + response.StatusCode);

            // for test use LoadData().
            var json = LoadData();
            //var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine("[..............] [ContractService] [GetContractsAsync] reponse json: " + json);
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Contract>>(json);
        }

        private static string LoadData()
        {
            Console.WriteLine("[..............] [ContractService] [LoadData] Ophalen stubdata");

            return @"[
                {
                    ""id"": 1,
                    ""personid"": 1,
                    ""product"": ""Personenauto"",
                    ""model"": ""KIA"",
                    ""type"": ""Ceed"",
                    ""licenseplate"": ""HF-067-X"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""200""
                },
                {
                    ""id"": 2,
                    ""personid"": 1,
                    ""product"": ""Personenauto"",
                    ""model"": ""Opel"",
                    ""type"": ""Astra"",
                    ""licenseplate"": ""69-SX-KZ"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""100""
                },
                {
                    ""id"": 3,
                    ""personid"": 1,
                    ""product"": ""Caravan"",
                    ""model"": ""KIP"",
                    ""type"": ""Roma"",
                    ""licenseplate"": ""8-XTB-36"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""100""
                }
            ]";
        }

    }
}
