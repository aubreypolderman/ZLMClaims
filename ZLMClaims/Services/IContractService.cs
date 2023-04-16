using ZLMClaims.Models;
namespace ZLMClaims.Services
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> GetContractsAsync();
    }
}
