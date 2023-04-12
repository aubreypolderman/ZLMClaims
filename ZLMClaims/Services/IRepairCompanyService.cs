using ZLMClaims.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
namespace ZLMClaims.Services
{
    public interface IRepairCompanyService
    {
        Task<IEnumerable<RepairCompany>> GetRepairCompaniesAsync();
        Task<RepairCompany> GetRepairCompanyByIdAsync(int id);

    }
}
