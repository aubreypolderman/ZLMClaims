using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Auth0;
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
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); 
            _httpClient.DefaultRequestHeaders.Authorization
             = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
#else                       
            _httpClient = new HttpClient();
#endif
        }

        public async Task<IEnumerable<ClaimForm>> GetAllClaimFormsByPersonIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/ClaimForms/user/{userId}");         
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize json response to Claim object
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ClaimForm>>(responseContent);
            }
            else
            {
                throw new HttpRequestException($"Error getting user with id {userId}: {response.ReasonPhrase}");
            }
        }

        public async Task<ClaimForm> GetClaimFormAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/ClaimForms/{id}");             
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ClaimForm>(responseContent);
                    
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new HttpRequestException($"Error getting claim form with id {id}: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw; // 
            }
        }


        public async Task<bool> SaveClaimFormAsync(ClaimForm claimForm)
        {
            try
            {
                var existingClaim = await GetClaimFormAsync((int)claimForm.Id);

                if (existingClaim != null)
                {
                    // Claim exists, so update the ClaimForm
                    await UpdateClaimFormAsync(claimForm);
                }
                else
                {
                    // Claim does not exist, so create a new ClaimForm
                    await CreateClaimFormAsync(claimForm);
                }

                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }


        public async Task UpdateClaimFormAsync(ClaimForm claimForm)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(claimForm); // Serialize the claimForm object to JSON

            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Create the HTTP request content with JSON body
            var response = await _httpClient.PutAsync($"https://10.0.2.2:7040/api/ClaimForms/{claimForm.Id}", content); // Make the PUT request to update the claimForm
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return; // The update was successful, so simply return
            }
            else
            {
                throw new HttpRequestException($"Error updating claimform with id {claimForm.Id}: {response.ReasonPhrase}");
            }
        }

        public async Task CreateClaimFormAsync(ClaimForm claimForm)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(claimForm); // Serialize the claimForm object to JSON
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Create the HTTP request content with JSON body
            var response = await _httpClient.PostAsync("https://10.0.2.2:7040/api/ClaimForms", content); // Make the POST request to create the claimForm
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return; // The creation was successful, so simply return
            }
            else
            {
                throw new HttpRequestException("Error creating claimform: " + responseContent);
            }
        }
    }
}
