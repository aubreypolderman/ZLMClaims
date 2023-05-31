using ZLMClaims.Models;
namespace ZLMClaims.Services
{
    public interface IClaimFormService
    {
        Task<IEnumerable<ClaimForm>> GetAllClaimFormsByPersonIdAsync(int personId);
        Task<HttpResponseMessage> CreateClaimFormAsync(ClaimForm claimForm);

    }
}
