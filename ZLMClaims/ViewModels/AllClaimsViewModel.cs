using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    public class AllClaimsViewModel : BindableObject   
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Claim> _claims;
        public ICommand NewClaimCommand { get; }

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

        public AllClaimsViewModel()
        {
            Console.WriteLine("[..............] [AllClaimsViewModel] Constructor");
            NewClaimCommand = new AsyncRelayCommand(NewClaimAsync);
        }

        private async Task NewClaimAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.ClaimFormStep1Page));
        }

        public async Task LoadDataAsync()
        {
            Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync]");

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

                // make the call
                var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/photos");
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] reponse: " + response);
                var content = await response.Content.ReadAsStringAsync();
                Claims = JsonConvert.DeserializeObject<ObservableCollection<Claim>>(content);
                
            } else
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] Internet connection is NOT available!!!!");
            }
            
        }

    }
}
