using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.Services;
public class ContractService : IContractService
{
    private readonly HttpClient _httpClient;

    public ContractService(HttpClient httpClient)
    {
        // check to see if _httpClient instance is not null
        Console.WriteLine("[..............] [ContractService] [constructor] httpclient injected ");
        //_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
#if DEBUG
        Console.WriteLine("[..............] [ContractService" +
            "] [constructor] httpclient debug -> use handler ");
        HttpsClientHandlerService handler = new HttpsClientHandlerService();
        _httpClient = new HttpClient(handler.GetPlatformMessageHandler()); // Assign the value to the class-level field
#else
            Console.WriteLine("[..............] [UserService] [constructor] ELSE new httpclient ");            
            _httpClient = new HttpClient();
#endif
    }

    public async Task<IEnumerable<Contract>> GetAllContractsByPersonIdAsync(int personId)
    {
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] Get all contracts for user with id " + personId);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] voor de call");

        var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Contracts");
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync]  response: " + response);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatusCode response: " + response.StatusCode);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] RequestMessage response: " + response.RequestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] reponsecontent: " + responseContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatuscodeSucces is all good! Return response");
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Contract>>(responseContent);
        }
        else
        {
            Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] Errortje getting contract for user with id {id} ");
            throw new HttpRequestException($"Error getting user with id {personId}: {response.ReasonPhrase}");
        }
    }

    public async Task<Contract> GetContractByIdAsync(int Id)
    {
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] Get contract with id " + Id);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] voor de call");

        var response = await _httpClient.GetAsync($"https://10.0.2.2:7040/api/Contracts/{Id}");
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync]  response: " + response);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatusCode response: " + response.StatusCode);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] ReasonPhrase response: " + response.ReasonPhrase);
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] RequestMessage response: " + response.RequestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] reponsecontent: " + responseContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] StatuscodeSucces is all good! Return response");
            return System.Text.Json.JsonSerializer.Deserialize<Contract>(responseContent);
        }
        else
        {
            Console.WriteLine("[..............] [ContractService] [GetAllContractsByPersonIdAsync] Errortje getting contract with id {id} ");
            throw new HttpRequestException($"Error getting user with id {Id}: {response.ReasonPhrase}");
        }
    }

    private static string LoadData()
    {
        Console.WriteLine("[..............] [ContractService] [LoadData] Ophalen stubdata");

        return @"[
                {
                    ""id"": 1,
                    ""personid"": 1,
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
                    ""personid"": 1,
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
                    ""personid"": 1,
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
