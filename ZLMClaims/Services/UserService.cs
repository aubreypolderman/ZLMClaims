using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
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
            Console.WriteLine("[..............] [UserService] [constructor] httpclient injected ");
            //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            #if DEBUG
                Console.WriteLine("[..............] [UserService] [constructor] httpclient debug -> use handler ");
                HttpsClientHandlerService handler = new HttpsClientHandlerService();
                _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field
            #else
                Console.WriteLine("[..............] [UserService] [constructor] ELSE new httpclient ");            
                _httpClient = new HttpClient();
            #endif
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] Get user with id " + id);
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] voor de call");

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Users/{id}");
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync]  response: " + response);
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] reponsecontent: " + responseContent);

            User user = null;
            if (response.IsSuccessStatusCode)
            { 
                Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] StatuscodeSucces is all good! Return response");
                //return JsonSerializer.Deserialize<User>(responseContent); 
                user = JsonSerializer.Deserialize<User>(responseContent);
                Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] deserialized user: " + user);
                Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] deserialized user email: " + user.Email);
                return user;
            }
            else
            {
                Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] Errortje getting user with id {id} ");
                throw new HttpRequestException($"Error getting user with id {id}: {response.ReasonPhrase}");
            }
        }

        private static string LoadData()
        {
            Console.WriteLine("[..............] [UserService] [LoadData] Ophalen stubdata");

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
    }
}
