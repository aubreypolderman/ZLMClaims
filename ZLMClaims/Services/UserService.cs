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
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] httpclient injected ");
            //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            #if DEBUG
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] httpclient debug -> use handler ");
                HttpsClientHandlerService handler = new HttpsClientHandlerService();
                _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field
            #else
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [constructor] ELSE new httpclient ");            
                _httpClient = new HttpClient();
            #endif
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Get user with id " + id);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] voor de call");

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Users/{id}");
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync]  response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] reponsecontent: " + responseContent);

            User user = null;
            if (response.IsSuccessStatusCode)
            { 
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] StatuscodeSucces is all good! Return response");
                //return JsonSerializer.Deserialize<User>(responseContent); 
                user = JsonSerializer.Deserialize<User>(responseContent);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] deserialized user: " + user);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] deserialized user email: " + user.Email);
                return user;
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByIdAsync] Errortje getting user with id {id} ");
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
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Get user with id " + email);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] voor de call");

            var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Users/email/{email}");
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync]  response: " + response);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] reponsecontent: " + responseContent);

            User user = null;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] StatuscodeSucces is all good! Return response");
                //return JsonSerializer.Deserialize<User>(responseContent); 
                user = JsonSerializer.Deserialize<User>(responseContent);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] deserialized user: " + user);
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] deserialized user email: " + user.Email);
                return user;
            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [UserService] [GetUserByEmailAsync] Errortje getting user with id {id} ");
                throw new HttpRequestException($"Error getting user with email {email}: {response.ReasonPhrase}");
            }
        }
    }
}
