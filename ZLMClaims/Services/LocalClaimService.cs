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
        if (Database != null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabaseFilename, Constants.Flags);
        var result = await Database.CreateTableAsync<Claim>();
    }

    public async Task<List<Claim>> GetClaims() 
    {
        Console.WriteLine("[..............] [LocalClaimService] [GetClaims] retrieve data from SQLite");
        await Init();
        return await Database.Table<Claim>().ToListAsync();
    }

    public async Task<int> SaveClaim(Claim claim)
    {
        await Init();
        if (claim.Id != 0)
            return await Database.UpdateAsync(claim);
        else
            return await Database.InsertAsync(claim);
    }

    public async Task<int> DeleteClaim(Claim claim)
    {
        await Init();
        return await Database.DeleteAsync(claim);
    }
}
