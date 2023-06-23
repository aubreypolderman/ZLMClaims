using IdentityModel;
using System.Net.Http.Headers;
using ZLMClaims.Auth0;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.Services;
public class ContractService : IContractService
{
    private readonly HttpClient _httpClient;
    private readonly string baseUrl = ApiUrlHelper.GetBaseApiUrl();

    public ContractService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization
                     = new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
    }

    public async Task<IEnumerable<Contract>> GetAllContractsByPersonIdAsync(int userId)
    {
        string apiUrl = $"{baseUrl}/api/Contracts/user/{userId}";
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {         
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Contract>>(responseContent);

        }
        else
        {         
            throw new HttpRequestException($"Error getting user with id {userId}: {response.ReasonPhrase}");
        }
    }

    public async Task<Contract> GetContractByIdAsync(int Id)
    {
        string apiUrl = $"{baseUrl}/api/Contracts/{Id}";
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Contract>(responseContent);
        }
        else
        {
            throw new HttpRequestException($"Error getting user with id {Id}: {response.ReasonPhrase}");
        }
    }
}
