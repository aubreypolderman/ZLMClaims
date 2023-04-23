using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Models;

namespace ZLMClaims.Services
{
    public class LocalClaimService : IClaimService
    {
        // see https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-7.0
        // // to setup SQLite with CRUD
        private SQLiteConnection connection;
        string _dbPath;
        public string errorMessage;

        private void Init()
        {
            // If there's already a connection, then there's no need to make a new one
            if (connection != null)
                return;

            connection = new SQLiteConnection(_dbPath);
            connection.CreateTable<Claim>();
        }

        public LocalClaimService(string dbPath)
        {
           
            _dbPath = dbPath;
        }

        public List<Claim> GetClaims() 
        {
            Console.WriteLine("[..............] [LocalClaimService] [GetClaims] retrieve data from SQLite");
            // Connect to SQLite, and retrievethe data. If fail, then setup message and return empy list
            try
            {
                Init();
                return connection.Table<Claim>().ToList();
            }
            catch (Exception) 
            {
                errorMessage = "Failed to retrieve data of local storage SQLite";
            }
            return new List<Claim>();
        }

        public Task<IEnumerable<Claim>> GetAllClaimsByPersonIdAsync(int personId)
        {
            Console.WriteLine("[..............] [LocalClaimService] [GetAllClaimsByPersonIdAsync] retrieve data from SQLite for personId: " + personId);
            var json = LoadData();

            // Deserialize json response to Claim object
            return (Task<IEnumerable<Claim>>)System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Claim>>(json);
        }

        private static string LoadData()
        {
            Console.WriteLine("[..............] [LocalClaimService] [LoadData] Ophalen stubdata");

            return @"[
               {
                  ""id"": 1,
                  ""dateOfOccurence"": ""2023-04-17T12:00:00Z"",
                  ""qWhatIsDamaged"": ""Front bumper"",
                  ""image1"":""https://static.zlm.nl/sites/default/files/2018-08/bromfietsverzekering.jpg"",
                  ""image1"":""https://static.zlm.nl/sites/default/files/2018-11/Oldtimerverzekering.jpg "",
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
                    ""model"": ""KIA"",
                    ""type"": ""Ceed"",
                    ""licenseplate"": ""HF-067-X"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""200""
                  }
                }

            ]";
        }
    }
}
