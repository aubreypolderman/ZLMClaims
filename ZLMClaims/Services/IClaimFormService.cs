using ZLMClaims.Models;
namespace ZLMClaims.Services
{
    public interface IClaimFormService
    {
        Task<IEnumerable<ClaimForm>> GetAllClaimFormsByPersonIdAsync(int userId);
        Task<ClaimForm> GetClaimFormAsync(int id);
        Task<bool> SaveClaimFormAsync(ClaimForm claimForm);
        Task UpdateClaimFormAsync(ClaimForm claimForm);
        Task CreateClaimFormAsync(ClaimForm claimForm);
        

    }
}
