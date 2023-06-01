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

        public async Task<IEnumerable<ClaimForm>> GetAllClaimFormsByPersonIdAsync(int userId)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimFormsByPersonIdAsync] Invoke api for userId " + userId);
            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/ClaimForms/user/{userId}");
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimFormsByPersonIdAsync] response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimFormsByPersonIdAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimFormsByPersonIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimFormsByPersonIdAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimFormsByPersonIdAsync] reponsecontent: " + responseContent);
            //var json = LoadData();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllClaimFormsByPersonIdAsync] StatuscodeSucces is all good! Return response");
                // Deserialize json response to Claim object
                return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ClaimForm>>(responseContent);
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimService] [GetAllContractsByPersonIdAsync] Errort getting contract for user with id {id} ");
                throw new HttpRequestException($"Error getting user with id {userId}: {response.ReasonPhrase}");
            }
        }

        public async Task<ClaimForm> GetClaimFormAsync(int id)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] Invoke api for claim with id = " + id);
            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/ClaimForms/{id}");
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] reponsecontent: " + responseContent);
            //var json = LoadData();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] StatuscodeSucces is all good! Return response");
                return System.Text.Json.JsonSerializer.Deserialize<ClaimForm>(responseContent);
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [GetClaimFormAsync] Errort getting claimform with id {id} ");
                throw new HttpRequestException($"Error getting user with id {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<bool> SaveClaimFormAsync(ClaimForm claimForm)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [SaveClaimFormAsync] Start ");
            try
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [SaveClaimFormAsync] Invoke GetClaimFormAsync with claim id " + claimForm.Id);
                var existingClaim = await GetClaimFormAsync(claimForm.Id);
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [SaveClaimFormAsync] After invoke GetClaimFormAsync with claim id " + claimForm.Id);

                if (existingClaim != null)
                {
                    // Claim exists, so update the ClaimForm
                    Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [SaveClaimFormAsync] Claim exists. Invoke UpdateClaimFormSync() ");
                    await UpdateClaimFormAsync(claimForm);
                }
                else
                {
                    // Claim does not exist, so create a new ClaimForm
                    Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [SaveClaimFormAsync] Claim does not exists. Invoke CreateClaimFormAsync() ");
                    await CreateClaimFormAsync(claimForm);
                }

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het opslaan van de claim: {ex.Message}");
                return false; 
            }
        }


        public async Task UpdateClaimFormAsync(ClaimForm claimForm)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [UpdateClaimFormAsync] ");
            var json = System.Text.Json.JsonSerializer.Serialize(claimForm); // Serialize the claimForm object to JSON

            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Create the HTTP request content with JSON body

            var response = await _httpClient.PutAsync($"https://10.0.2.2:7040/api/ClaimForms/{claimForm.Id}", content); // Make the PUT request to update the claimForm

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [UpdateClaimFormAsync] reponsecontent: " + responseContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [UpdateClaimFormAsync] StatuscodeSucces is all good! Return response");
                return; // The update was successful, so simply return
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [UpdateClaimFormAsync] Error updating claimform with id {id} ");
                throw new HttpRequestException($"Error updating claimform with id {claimForm.Id}: {response.ReasonPhrase}");
            }
        }


        public async Task CreateClaimFormAsync(ClaimForm claimForm)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] Start ");

            var json = System.Text.Json.JsonSerializer.Serialize(claimForm); // Serialize the claimForm object to JSON

            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Create the HTTP request content with JSON body

            var response = await _httpClient.PostAsync("https://10.0.2.2:7040/api/ClaimForms", content); // Make the POST request to create the claimForm

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] responseContent: " + responseContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] StatuscodeSucces is all good! ClaimForm created successfully.");
                return; // The creation was successful, so simply return
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormService] [CreateClaimFormAsync] Error creating claimform: " + responseContent);
                throw new HttpRequestException("Error creating claimform: " + responseContent);
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
