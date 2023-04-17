using ZLMClaims.Models;
namespace ZLMClaims.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<Claim>> GetAllClaimsByPersonIdAsync(int personId);
    }
}
