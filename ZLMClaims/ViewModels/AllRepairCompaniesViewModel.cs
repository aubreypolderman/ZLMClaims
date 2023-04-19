using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    public class AllRepairCompaniesViewModel : BaseViewModel   
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<RepairCompany> _repaircompanies;
        private Command<object> tapCommand;
        public ICommand SelectRepairCompanyCommand { get; }
        private INavigation navigation;

        public ObservableCollection<RepairCompany> RepairCompanies
        {
            get => _repaircompanies;
            set
            {
                _repaircompanies = value;
                OnPropertyChanged();
            }
        }

        public Command GetRepairCompaniesCommand { get; }

        public AllRepairCompaniesViewModel()
        {
            Console.WriteLine("[AllRepairCompaniesViewModel] [==============] Constructor");
            
        }

        public AllRepairCompaniesViewModel(INavigation _navigation)
        {
            Console.WriteLine("[AllRepairCompaniesViewModel] [==============] Constructor");
            SelectRepairCompanyCommand = new AsyncRelayCommand<ViewModels.AllRepairCompaniesViewModel>(SelectRepairCompanyAsync);
        }

        private async Task SelectRepairCompanyAsync(ViewModels.AllRepairCompaniesViewModel repaircompany)
        {
            // if (repaircompany != null) 
            //     await Shell.Current.GoToAsync($"{nameof(RepairCompanyPage)}?load={repaircompany.Identifier}");
            Console.WriteLine("[SelectRepairCompanyAsync] [==============] repaircompany:" + repaircompany);
        }


        public async Task LoadDataAsync()
        {
            Console.WriteLine("[AllRepairCompaniesViewModel] [LoadDataAsync ][==============] ");

            // var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/users");

            string response = @"[
                {
                    ""id"": 1,
                    ""name"": ""Renova"",
                    ""username"": ""Renova"",
                    ""email"": ""rcc@renova.nl"",
                    ""address"": {
                        ""street"": ""Amundsenweg"",
                        ""suite"": ""33"",
                        ""city"": ""Goes"",
                        ""zipcode"": ""4462 GP"",
                        ""geo"": {
                            ""lat"": ""-37.3159"",
                            ""lng"": ""81.1496""
                        }
                    },
                    ""phone"": ""0113 - 2454 225"",
                    ""website"": ""www.renova.nl"",
                    ""company"": {
                        ""name"": ""Renova"",
                        ""catchPhrase"": ""Merk schadeherstelbedrijf (MINI)"",
                        ""bs"": ""harness real-time e-markets""
                    }
                },
                {
                    ""id"": 2,
                    ""name"": ""Van Mossel Autoschade"",
                    ""username"": ""Van Mossel"",
                    ""email"": ""info.autoschade.middelburg@vanmossel.nl"",
                    ""address"": {
                        ""street"": ""Klarinetweg"",
                        ""suite"": ""4"",
                        ""city"": ""Middelburg"",
                        ""zipcode"": ""4337 RA"",
                        ""geo"": {
                            ""lat"": ""-43.9509"",
                            ""lng"": ""-34.4618""
                        }
                    },
                    ""phone"": ""0118 - 613 564"",
                    ""website"": ""www.schadenetvanmossel.nl/middelburg"",
                    ""company"": {
                        ""name"": ""Van Mossel Autoschade"",
                        ""catchPhrase"": ""Universeel schaddeherstelbedrijf"",
                        ""bs"": ""synergize scalable supply-chains""
                    }
                },
 {
                    ""id"": 3,
                    ""name"": ""Van den Berg Autoschade"",
                    ""username"": ""Van den Berg"",
                    ""email"": ""info@vandenbergautoschade.nl"",
                    ""address"": {
                        ""street"": ""Hermesweg"",
                        ""suite"": ""5"",
                        ""city"": ""Vlissingen"",
                        ""zipcode"": ""4382 ND"",
                        ""geo"": {
                            ""lat"": ""-43.9509"",
                            ""lng"": ""-34.4618""
                        }
                    },
                    ""phone"": ""0118 - 414 329"",
                    ""website"": ""www.vandenbergautoschade.nl"",
                    ""company"": {
                        ""name"": ""Van den Berg Autoschade"",
                        ""catchPhrase"": ""Universeel schaddeherstelbedrijf"",
                        ""bs"": ""synergize scalable supply-chains""
                    }
                }
            ]";
            Console.WriteLine("[AllRepairCompaniesViewModel] [LoadDataAsync] [==============] reponse: " + response);
            //var content = await response.Content.ReadAsStringAsync();
            RepairCompanies = JsonConvert.DeserializeObject<ObservableCollection<RepairCompany>>(response);


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
            Console.WriteLine("[AllRepairCompaniesViewModel] [OnTapped ][==============] ");
            Console.WriteLine("[AllRepairCompaniesViewModel] [OnTapped ][==============] object => "+ obj);
            //var newPage = new RepairCompanyPage();
            //newPage.BindingContext = obj;
            //Navigation.PushAsync(newPage);
        }
    }
}
