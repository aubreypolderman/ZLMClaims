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
    public class AllRepairCompaniesViewModel : BindableObject   
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
            Console.WriteLine("[..............] [AllRepairCompaniesViewModel] No args constructor");
            
        }

        public AllRepairCompaniesViewModel(INavigation _navigation)
        {
            Console.WriteLine("[..............] [AllRepairCompaniesViewModel] Navigation constructor");
            SelectRepairCompanyCommand = new AsyncRelayCommand<ViewModels.AllRepairCompaniesViewModel>(SelectRepairCompanyAsync);
        }

        private async Task SelectRepairCompanyAsync(ViewModels.AllRepairCompaniesViewModel repaircompany)
        {
            // if (repaircompany != null) 
            //     await Shell.Current.GoToAsync($"{nameof(RepairCompanyPage)}?load={repaircompany.Identifier}");
            Console.WriteLine("[..............] [AllRepairCompaniesViewModel] [SelectRepairCompanyAsync] repaircompany:" + repaircompany);
        }
        

       public async Task LoadDataAsync()
        {
            Console.WriteLine("[AllRepairCompaniesViewModel] [LoadDataAsync ][==============] ");
            var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/users");
            Console.WriteLine("[AllRepairCompaniesViewModel] [LoadDataAsync] [==============] reponse: " + response);
            var content = await response.Content.ReadAsStringAsync();
            RepairCompanies = JsonConvert.DeserializeObject<ObservableCollection<RepairCompany>>(content);
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
