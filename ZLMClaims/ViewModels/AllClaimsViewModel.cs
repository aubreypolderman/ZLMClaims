using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaims.ViewModels
{
    public class AllClaimsViewModel : BindableObject   
    {
        private ObservableCollection<Claim> _claims;
        public ICommand NewClaimCommand { get; }
        INavigationService navigationService;
        IClaimService claimService;

        // By making the claims observable, the view is automatically refreshed whenever a change occure
        public ObservableCollection<Claim> Claims
        {
            get => _claims;
            set
            {
                _claims = value;
                OnPropertyChanged();
            }
        }

        public Command GetClaimsCommand { get; }

        public AllClaimsViewModel(INavigationService navigationService, IClaimService claimService)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [constructor] Navigation and IClaimService injected");
            this.navigationService = navigationService;
            this.claimService = claimService;

            LoadDataAsync();

        }
        // Workaround: empty constructor so the allclaimspage.xaml gets compiled. This does not however make the page show the claims...
        public AllClaimsViewModel() { }

        public async Task LoadDataAsync()
        {
            Console.WriteLine("[AllClaimsViewModel] [LoadDataAsync] ");

            // Check internet connection
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;
            Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] profiles => " + profiles);

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] WiFi connection is available => " + accessType);
            }


            if (accessType == NetworkAccess.Internet)
            {
                
                // Connection to internet is available
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] Internet connection is available => " + accessType);
                var claims = await claimService.GetAllClaimsByPersonIdAsync(1);
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] claimService invoked...");
                foreach (var claim in claims)
                {
                    Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] Claim on contract:" + claim.Contract.Product);
                    Claims.Add(claim);
                }

            } else
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] Internet connection is NOT available!!!!");
            }
            
        }

    }
}
