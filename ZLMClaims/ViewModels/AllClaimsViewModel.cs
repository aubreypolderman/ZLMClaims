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
        IDialogService dialogService;
        IConnectivity connectivityService;

        // By making the claims observable, the view is automatically refreshed whenever a change occure
        public ObservableCollection<Claim> Claims { get; private set; } = new();

        public AllClaimsViewModel(INavigationService navigationService, IClaimService claimService, IDialogService dialogService, IConnectivity connectivityService)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [constructor] Navigation and IClaimService injected");
            this.navigationService = navigationService;
            this.claimService = claimService;
            this.dialogService = dialogService;
            this.connectivityService = connectivityService;

            GetAllClaims();   
        }

        [RelayCommand]
        public async Task GetAllClaims()
        {
            Console.WriteLine("[..............] [AllClaimsViewModel] [GetAllClaims] ");

            // Check WiFi connection
            IEnumerable<ConnectionProfile> profiles = connectivityService.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [GetAllClaims] WiFi connection is available");
            }

            // Check internet connection
            NetworkAccess accessType = connectivityService.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                // Connection to internet is available
                Console.WriteLine("[..............] [AllClaimsViewModel] [GetAllClaims] Internet connection is available");

                if (Claims.Any()) Claims.Clear();
                var claims = await claimService.GetAllClaimsByPersonIdAsync(1);
               
                foreach (var claim in claims)
                {
                    Console.WriteLine("[..............] [AllClaimsViewModel] [GetAllClaims] Claim on contract:" + claim.Contract.Product);
                    Claims.Add(claim);
                }

            }
            else
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [GetAllClaims] Internet connection is NOT available!!!!");
                await dialogService.DisplayAlertAsync("Error", "Internet connection is NOT available!", "OK");
            }

        }

    }
}
