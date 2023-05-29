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
    public class ClaimFormService : IClaimFormService
    {
        private readonly HttpClient _httpClient;
        public string errorMessage;

        public ClaimFormService(HttpClient httpClient )
        {
            // check to see if _httpClient instance is not null
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [constructor] httpclient injected ");
            //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
#if DEBUG
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService" +
                "] [constructor] httpclient debug -> use handler ");
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field
#else
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] ELSE new httpclient ");            
            _httpClient = new HttpClient();
#endif
        }

        public async Task<IEnumerable<Claim>> GetAllClaimFormsByPersonIdAsync(int personId)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimsAsync] Invoke api for personId " + personId);
            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/ClaimForms");
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimsAsync] response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimsAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimsAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimsAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimsAsync] reponsecontent: " + responseContent);
            //var json = LoadData();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllContractsByPersonIdAsync] StatuscodeSucces is all good! Return response");
                // Deserialize json response to Claim object
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Claim>>(responseContent);
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllContractsByPersonIdAsync] Errortje getting contract for user with id {id} ");
                throw new HttpRequestException($"Error getting user with id {personId}: {response.ReasonPhrase}");
            }
        }

        private static string LoadData()
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [LoadData] Ophalen stubdata");

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
