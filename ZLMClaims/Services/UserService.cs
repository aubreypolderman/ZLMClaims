﻿using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Models;

namespace ZLMClaims.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            // check to see if _httpClient instance is not null
            Console.WriteLine("[..............] [UserService] [constructor] httpclient injhected ");
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] Get user with id " + id);

            var json = LoadData();

            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] reponse json: " + json);

            return JsonSerializer.Deserialize<User>(json);
            /*
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] StatusCode response: " + response.StatusCode);
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] RequestMessage response: " + response.RequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] reponsecontent: " + responseContent);

            if (response.IsSuccessStatusCode)
            { 
                Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] StatuscodeSucces is all good! Return response");
                return JsonSerializer.Deserialize<User>(responseContent); 
            }
            else
            {
                Console.WriteLine("[..............] [UserService] [GetUserByIdAsync] Errortje getting user with id {id} ");
                throw new HttpRequestException($"Error getting user with id {id}: {response.ReasonPhrase}");
            }
            */
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
				""address"": {
					""street"": ""Cirkel"",
					""housenumber"": ""63"",
					""city"": ""Vlissingen"",
					""zipcode"": ""4384DS""
                },
				""phone"": ""06-12345678""
			}
]";
        }
    }
}
