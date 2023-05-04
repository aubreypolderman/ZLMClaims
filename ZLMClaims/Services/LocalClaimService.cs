using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ZLMClaims.Models;

namespace ZLMClaims.Services;

public class LocalClaimService : ILocalClaimService
{
    // see https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-7.0
    // to setup SQLite with CRUD
    private SQLiteAsyncConnection Database;

    public LocalClaimService()
    {
    }

    async Task Init()
    {
        // If there's already a connection, then there's no need to make a new one
        Console.WriteLine("[..............] [LocalClaimService] [Init] Check for existing database connection");
        if (Database != null)
        {
            Console.WriteLine("[..............] [LocalClaimService] [Init] Connection exists!");
            return;
        }
        Console.WriteLine("[..............] [LocalClaimService] [Init] Connection doesn't exists");
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Claim>();
        Console.WriteLine("[..............] [LocalClaimService] [Init]New connections was made with result "+result);
        return;
    }

    public async Task<List<Claim>> GetClaims() 
    {
        Console.WriteLine("[..............] [LocalClaimService] [GetClaims] retrieve data from SQLite");
        await Init();
        return await Database.Table<Claim>().ToListAsync();
    }

    public async Task<int> SaveClaim(Claim claim)
    {
        Console.WriteLine("[..............] [LocalClaimService] [SaveClaim] Save claim with licenseplate "+ claim.Contract.LicensePlate);
        await Init();
        if (claim.Id != 0)
        {
            Console.WriteLine("[..............] [LocalClaimService] [SaveClaim] Update claim with id "+claim.Id);
            var result = await Database.UpdateAsync(claim);
            Console.WriteLine("[..............] [LocalClaimService] [SaveClaim] Claim updated with result " + result);
            return result;
        }
        else
        {
            Console.WriteLine("[..............] [LocalClaimService] [SaveClaim] Insert claim ");
            var result = await Database.InsertAsync(claim);
            Console.WriteLine("[..............] [LocalClaimService] [SaveClaim] Claim inserted with result " + result);
            return result;
        }
    }

    public async Task<int> DeleteClaim(Claim claim)
    {
        await Init();
        return await Database.DeleteAsync(claim);
    }
}
