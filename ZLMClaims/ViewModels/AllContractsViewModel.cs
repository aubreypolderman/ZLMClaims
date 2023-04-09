using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    public class AllContractsViewModel : BindableObject   
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Contract> _contracts;
        private Command<object> tapCommand;
        public ICommand SelectContractCommand { get; }
        private INavigation navigation;

        public ObservableCollection<Contract> Contracts
        {
            get => _contracts;
            set
            {
                _contracts = value;
                OnPropertyChanged();
            }
        }

        public Command GetContractCommand { get; }

        public AllContractsViewModel()
        {
            Console.WriteLine("[..............] [AllContractsViewModel] No args constructor");
            
        }

        public AllContractsViewModel(INavigation _navigation)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] Navigation constructor");
            SelectContractCommand = new AsyncRelayCommand<ViewModels.AllContractsViewModel>(SelectContractAsync);
        }

        private async Task SelectContractAsync(ViewModels.AllContractsViewModel contract)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [SelectRepairCompanyAsync] contract:" + contract);
        }
        

       public async Task LoadDataAsync()
        {
            Console.WriteLine("[AllContractsViewModel] [LoadDataAsync ][==============] ");
            /* Comment because of weird certificate messages
            var response = await _client.GetAsync("https://10.0.2.2:7040/api/Contracts");
            Console.WriteLine("[AllContractsViewModel] [LoadDataAsync] [==============] reponse: " + response);
            var content = await response.Content.ReadAsStringAsync();
            Contracts = JsonConvert.DeserializeObject<ObservableCollection<Contract>>(content);
            */

            /* use hardcoded data for now */
            // Hardcoded test data
            Contracts = new ObservableCollection<Contract>
            {                 
                new Contract { Id = 1, PersonId = 1, Product = "Personenauto", Model = "KIA", Type = "Ceed",LicensePlate = "HF-067-X", DamageFreeYears = 10, StartingDate = DateTime.Now.AddDays(-365), EndDate = DateTime.Now.AddDays(+100), AnnualPolicyPremium = 200},
                new Contract { Id = 2, PersonId = 1, Product = "Personenauto", Model = "Opel", Type = "Astra Twintop",LicensePlate = "69-SX-KZ", DamageFreeYears = 10, StartingDate = DateTime.Now.AddDays(-1000), EndDate = DateTime.Now.AddDays(+100), AnnualPolicyPremium = 100},
                new Contract { Id = 3, PersonId = 1, Product = "Caravan", Model = "Kip", Type = "Roma",LicensePlate = "8-XTB-36", DamageFreeYears = 5, StartingDate = DateTime.Now.AddDays(-500), EndDate = DateTime.Now.AddDays(+100), AnnualPolicyPremium = 75},
                new Contract { Id = 4, PersonId = 1, Product = "Snorfiets", Model = "Berini", Type = "Napoli II",LicensePlate = "8-DLD-36", DamageFreeYears = 5, StartingDate = DateTime.Now.AddDays(-500), EndDate = DateTime.Now.AddDays(+100), AnnualPolicyPremium = 50}
            };
        }

        public Command<object> TapCommand
        {
            get { return tapCommand; }
            set { tapCommand = value; }
        }

        public INavigation Navigation
        {
            get { return navigation; }
            set { navigation = value; }
        }

        private void OnTapped(object obj)
        {
            Console.WriteLine("[AllContractsViewModel] [OnTapped ][==============] ");
            Console.WriteLine("[AllContractsViewModel] [OnTapped ][==============] object => " + obj);
        }
    }
}
