﻿using CommunityToolkit.Mvvm.Input;
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
                /* Placeholder to test actual https rest api
                var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/photos");
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] reponse: " + response);
                var content = await response.Content.ReadAsStringAsync();
                Claims = JsonConvert.DeserializeObject<ObservableCollection<Claim>>(content);
                */
                Claims = new ObservableCollection<Claim>
                {
                    new Claim { Id = 1, ContractId = 1, CauseOfDamage = "Eenzijdig", ExplanationOfWhatHappened = "Laaghangende zon", LongitudeAccident = 123456,LatitudeAccident = 112213, ClaimDateTime = DateTime.Now.AddDays(1), Image1 = "https://media.gettyimages.com/id/126375033/nl/foto/m6-motorway-and-howgill-fells-at-night.jpg?s=612x612&w=0&k=20&c=WXEoK9vK0k4WkDIM_fxQYzLG0XllFr-5WDPl2j8e8hA=", Image2 = "data:image/jpeg;base64,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"},
                    new Claim { Id = 2, ContractId = 2, CauseOfDamage = "Eenzijdig", ExplanationOfWhatHappened = "Vandalisme", LongitudeAccident = 123456,LatitudeAccident = 112213, ClaimDateTime = DateTime.Now.AddDays(1), Image1 = "https://media.gettyimages.com/id/126375033/nl/foto/m6-motorway-and-howgill-fells-at-night.jpg?s=612x612&w=0&k=20&c=WXEoK9vK0k4WkDIM_fxQYzLG0XllFr-5WDPl2j8e8hA=", Image2 = "data:image/jpeg;base64,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"},
                };

            } else
            {
                Console.WriteLine("[..............] [AllClaimsViewModel] [LoadDataAsync] Internet connection is NOT available!!!!");
            }
            
        }

    }
}
