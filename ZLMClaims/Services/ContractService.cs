using System.Net.Http.Headers;
using ZLMClaims.Auth0;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.Services;
public class ContractService : IContractService
{
    private readonly HttpClient _httpClient;

    public ContractService(HttpClient httpClient)
    {
#if DEBUG
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [Constructor] DEBUG with TokenHolder.AccessToken = " + TokenHolder.AccessToken);
        _httpClient = httpClient; //👈 new code
        HttpsClientHandlerService handler = new HttpsClientHandlerService();
        _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field

        // new use TokenHandler.AccessToken
        _httpClient.DefaultRequestHeaders.Authorization
         = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [Constructor] TokenHolder.AccessToken = " + TokenHolder.AccessToken);

#else
            Console.WriteLine(DateTime.Now + "[..............] [ContractService] [Constructor] ELSE with TokenHolder.AccessToken = " + TokenHolder.AccessToken);
            Console.WriteLine(DateTime.Now + "[..............] [ContractService] [Constructor] ELSE " + _httpClient.DefaultRequestHeaders.Authorization);
            _httpClient = new HttpClient();
#endif
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [Constructor] End ");
    }

    public async Task<IEnumerable<Contract>> GetAllContractsByPersonIdAsync(int userId)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] Get all contracts for user with id " + userId);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] voor de call");

        //var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Contracts/user/{userId}");
        string ApiUrl = "https://10.0.2.2:7040/api/Contracts/user/" + userId;
        HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);

        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] response: " + response);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatusCode response: " + response.StatusCode);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] RequestMessage response: " + response.RequestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] reponsecontent: " + responseContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatuscodeSucces is all good! Return response");
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Contract>>(responseContent);
        }
        else
        {
            Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] Errortje getting contract for user with id {id} ");
            throw new HttpRequestException($"Error getting user with id {userId}: {response.ReasonPhrase}");
        }
    }

    public async Task<Contract> GetContractByIdAsync(int Id)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] Get contract with id " + Id);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] voor de call");

        // var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Contracts/{Id}");
        string ApiUrl = "https://10.0.2.2:7040/api/Contracts/" + Id;
        HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync]  response: " + response);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatusCode response: " + response.StatusCode);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] RequestMessage response: " + response.RequestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] reponsecontent: " + responseContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatuscodeSucces is all good! Return response");
            return System.Text.Json.JsonSerializer.Deserialize<Contract>(responseContent);
        }
        else
        {
            Console.WriteLine(DateTime.Now + "[..............] [ContractService] [GetAllContractsByPersonIdAsync] Errortje getting contract with id {id} ");
            throw new HttpRequestException($"Error getting user with id {Id}: {response.ReasonPhrase}");
        }
    }

    private static string LoadData()
    {
        Console.WriteLine(DateTime.Now + "[..............] [ContractService] [LoadData] Ophalen stubdata");

        return @"[
                {
                    ""id"": 1,
                    ""userid"": 1,
                    ""product"": ""Personenauto"",
                    ""make"": ""KIA"",
                    ""model"": ""Ceed"",
                    ""licenseplate"": ""HF-067-X"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""200""
                },
                {
                    ""id"": 2,
                    ""userid"": 1,
                    ""product"": ""Personenauto"",
                    ""make"": ""Opel"",
                    ""model"": ""Astra"",
                    ""licenseplate"": ""69-SX-KZ"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""100""
                },
                {
                    ""id"": 3,
                    ""userid"": 1,
                    ""product"": ""Caravan"",
                    ""make"": ""KIP"",
                    ""model"": ""Roma"",
                    ""licenseplate"": ""8-XTB-36"",
                    ""damagefreeyears"": ""5"",
                    ""startingdate"": ""2023-01-01"",
                    ""enddate"": ""2024-01-01"",
                    ""annualpolicypremium"": ""100""
                }
            ]";
    }

}
