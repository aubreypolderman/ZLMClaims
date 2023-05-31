using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
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

#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field
#else           
            _httpClient = new HttpClient();
#endif
        }

        public async Task<IEnumerable<ClaimForm>> GetAllClaimFormsByPersonIdAsync(int personId)
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
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ClaimForm>>(responseContent);
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllContractsByPersonIdAsync] Errort getting contract for user with id {id} ");
                throw new HttpRequestException($"Error getting user with id {personId}: {response.ReasonPhrase}");
            }
        }

        public async Task<HttpResponseMessage> CreateClaimFormAsync(ClaimForm claimForm)
        {           
            var json = JsonConvert.SerializeObject(claimForm);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] Serialize to Json: " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://10.0.2.2:7040/api/ClaimForms", content);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] Response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] RequestMessage response: " + response.RequestMessage);

            return response;
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
