using ZLMClaims.Models;
namespace ZLMClaims.Services
{
    public interface IClaimFormService
    {
        Task<IEnumerable<Claim>> GetAllClaimFormsByPersonIdAsync(int personId);
    }
}
