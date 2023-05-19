using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Models;

namespace ZLMClaims.Services
{
    public class ClaimService : IClaimService
    {
        private readonly HttpClient _httpClient;
        public string errorMessage;

        public ClaimService(HttpClient httpClient )
        {
            // check to see if _httpClient instance is not null    
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Claim>> GetAllClaimsByPersonIdAsync(int personId)
        {
            Console.WriteLine("[..............] [ClaimService] [GetAllClaimsAsync] Invoke api for personId " + personId);

            // online
            /*
            // Make API call to retrieve all claims by personID
            HttpResponseMessage response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

            // Handle non-successful response codes
            response.EnsureSuccessStatusCode();
            Console.WriteLine("[..............] [ContractService] [GetAllClaimsAsync] statuscode: " + response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            */

            // offline
            // for testpurpose only
            var json = LoadData();
            
            Console.WriteLine("[..............] [ClaimService] [GetAllClaimsAsync] reponse json: " + json);
            // Deserialize json response to Claim object
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Claim>>(json);
        }

        private static string LoadData()
        {
            Console.WriteLine("[..............] [ClaimService] [LoadData] Ophalen stubdata");

            return @"[
               {
                  ""id"": 1,
                  ""dateOfOccurence"": ""2023-04-17T12:00:00Z"",
                 
                  ""image1"":""https://static.zlm.nl/sites/default/files/2018-08/bromfietsverzekering.jpg"",
                  ""image2"":""https://static.zlm.nl/sites/default/files/2018-11/Oldtimerverzekering.jpg "",
                  ""images"": [
                    ""https://example.com/image1.jpg"",
                    ""https://example.com/image2.jpg"",
                    ""https://example.com/image3.jpg"",
                    ""https://example.com/image4.jpg"",
                    ""https://example.com/image5.jpg"",
                    ""https://example.com/image6.jpg"",
                    ""https://example.com/image7.jpg"",
                    ""https://example.com/image8.jpg"",
                    ""https://example.com/image9.jpg"",
                    ""https://example.com/image10.jpg""
                  ],
                  ""contract"": {
                    ""id"": 1,
                    ""personid"": 1,
                    ""product"": ""Personenauto"",
                    ""make"": ""KIA"",
                    ""model"": ""Ceed"",
                    ""type"": ""1.8"",
                    ""licenseplate"": ""HF-067-X"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""200""
                  },
				  ""accidentAddress"": {
				    ""street"": ""Poelendaeleweg"",
                    ""suite"": ""21"",
				    ""city"": ""Middelburg"",
				    ""zipcode"": ""4335HM"",
				    ""geo"": {
                        ""latitude"": 51.45956502251781,
				        ""longitude"": 3.570303055656289
                        }
                    }
                  }
            ]";	
        }
    }
}
