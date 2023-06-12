using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Auth0;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
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

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Users/{id}");            
            var responseContent = await response.Content.ReadAsStringAsync();

            User user = null;
            if (response.IsSuccessStatusCode)
            { 
                user = JsonSerializer.Deserialize<User>(responseContent);
                return user;
            }
            else
            {
                throw new HttpRequestException($"Error getting user with id {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Users/email/{email}");
            var responseContent = await response.Content.ReadAsStringAsync();

            User user = null;
            if (response.IsSuccessStatusCode)
            {
                user = JsonSerializer.Deserialize<User>(responseContent);
                return user;
            }
            else
            {
                throw new HttpRequestException($"Error getting user with email {email}: {response.ReasonPhrase}");
            }
        }
    }
}
