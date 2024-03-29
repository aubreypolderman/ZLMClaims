﻿using ZLMClaims.Models;
namespace ZLMClaims.Services
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> GetAllContractsByPersonIdAsync(int personId);
        Task<Contract> GetContractByIdAsync(int Id);
    }
}
