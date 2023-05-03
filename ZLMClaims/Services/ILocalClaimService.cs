using ZLMClaims.Models;
namespace ZLMClaims.Services;

public interface ILocalClaimService
{
    Task<List<Claim>> GetClaims();
    Task<int> SaveClaim(Claim claim);
    Task<int> DeleteClaim(Claim claim);
}
