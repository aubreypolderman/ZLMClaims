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
        IClaimFormService claimFormService;
        IDialogService dialogService;
        IConnectivity connectivityService;
        ILocalClaimService localClaimService;

        // By making the claims observable, the view is automatically refreshed whenever a change occure
        public ObservableCollection<Claim> Claims { get; private set; } = new();

        public AllClaimsViewModel(INavigationService navigationService, IClaimFormService claimFormService, IDialogService dialogService, IConnectivity connectivityService, ILocalClaimService localClaimService)
        {
            this.navigationService = navigationService;
            this.claimFormService = claimFormService;
            this.dialogService = dialogService;
            this.connectivityService = connectivityService;
            this.localClaimService = localClaimService;
        }

        [RelayCommand]
        public async Task GetAllClaims()
        {
            Console.WriteLine(DateTime.Now + "[..............] [AllClaimsViewModel] [GetAllClaims] ");

            // retrieve the userid from the preference set        
            int userId = Preferences.Default.Get("userId", -1);

            // Check WiFi connection
            IEnumerable<ConnectionProfile> profiles = connectivityService.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                Console.WriteLine(DateTime.Now + "[..............] [AllClaimsViewModel] [GetAllClaims] WiFi connection is available");
            }

            // Check internet connection
            NetworkAccess accessType = connectivityService.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                // Connection to internet is available
                Console.WriteLine(DateTime.Now + "[..............] [AllClaimsViewModel] [GetAllClaims] Internet connection is available");

                if (Claims.Any()) Claims.Clear();
                var claims = await claimFormService.GetAllClaimFormsByPersonIdAsync(userId);
                //var claims = await localClaimService.GetClaims();  

                foreach (var claim in claims)
                {
                    Console.WriteLine(DateTime.Now + "[..............] [AllClaimsViewModel] [GetAllClaims] Claim on contract:" + claim.Contract.Product);
                    Claims.Add(claim);
                }

            }
            else
            {
                Console.WriteLine(DateTime.Now + "[..............] [AllClaimsViewModel] [GetAllClaims] Internet connection is NOT available!!!!");
                await dialogService.DisplayAlertAsync("Error", "Internet connection is NOT available!", "OK");
            }

        }

    }
}
