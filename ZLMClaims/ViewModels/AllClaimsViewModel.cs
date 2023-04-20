using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.ViewModels
{
    public partial class AllClaimsViewModel : BaseViewModel
    {
        public ObservableCollection<Claim> _claims;
        
        INavigationService navigationService;
        IClaimService claimService;

        // By making the claims observable, the view is automatically refreshed whenever a change occure
        public ObservableCollection<Claim> Claims { get; private set; } = new();

        public AllClaimsViewModel(INavigationService navigationService, IClaimService claimService)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [constructor] Navigation and IClaimService injected");
            this.navigationService = navigationService;
            this.claimService = claimService;

            GetAllClaims();
        }

        [RelayCommand]
        public async Task GetAllClaims()
        {
            Console.WriteLine("[..............] [AllClaimsViewModel] [LoadData] ");

            // Check internet connection
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;
            
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadData] WiFi connection is available => " + accessType);
            }


            if (accessType == NetworkAccess.Internet)
            {
                // Connection to internet is available
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadData] Internet connection is available => " + accessType);

                if (Claims.Any()) Claims.Clear();
                var claims = await claimService.GetAllClaimsByPersonIdAsync(1);
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadData] claimService invoked...");
                foreach (var claim in claims)
                {
                    Console.WriteLine("[..............] [AllClaimsViewModel] [LoadData] Claim on contract:" + claim.Contract.Product);
                    Claims.Add(claim);
                }

            }
            else
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadData] Internet connection is NOT available!!!!");
            }

        }

    }
}
