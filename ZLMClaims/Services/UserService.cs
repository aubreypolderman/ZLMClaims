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
            // check to see if _httpClient instance is not null
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] httpclient injected ");
            
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field
            _httpClient.DefaultRequestHeaders.Authorization
             = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] TokenHolder.AccessToken => " + TokenHolder.AccessToken);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] _httpClient.DefaultRequestHeaders.Authorization => " + _httpClient.DefaultRequestHeaders.Authorization);
#else
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] ELSE new httpclient ");            
                _httpClient = new HttpClient();
#endif
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] httpclient debug ->klaar ");
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Get user with id " + id);

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Users/{id}");            
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Reponsecontent: " + responseContent);

            User user = null;
            if (response.IsSuccessStatusCode)
            { 
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] StatuscodeSucces is all good! Return response");
                //return JsonSerializer.Deserialize<User>(responseContent); 
                user = JsonSerializer.Deserialize<User>(responseContent);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Deserialized user: " + user);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Deserialized user email: " + user.Email);
                return user;
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Error getting user with id {id} ");
                throw new HttpRequestException($"Error getting user with id {id}: {response.ReasonPhrase}");
            }
        }

        private static string LoadData()
        {
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [LoadData] Ophalen stubdata");

            return @"[
			{
				""id"": 1,
			    ""name"": ""Aubrey Polderman"",
				""username"": ""aubreypolderman@gmail.com"",
				""email"": ""aubreypolderman@gmail.com"",
                ""phone"": ""06-12345678"",
				""street"": ""Cirkel"",
				""housenumber"": ""63"",
				""city"": ""Vlissingen"",
				""zipcode"": ""4384DS"",
                ""latitude"": ""51.461684899386995"",
				""longitude"": ""3.5559567820729203""
				
			}
]";
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Get user with email " + email);

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Users/email/{email}");
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Reponsecontent: " + responseContent);

            User user = null;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] StatuscodeSucces is all good! Return response");
                //return JsonSerializer.Deserialize<User>(responseContent); 
                user = JsonSerializer.Deserialize<User>(responseContent);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Deserialized user: " + user);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Deserialized user email: " + user.Email);
                return user;
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Error getting user with id {id} ");
                throw new HttpRequestException($"Error getting user with email {email}: {response.ReasonPhrase}");
            }
        }
    }
}
