using Newtonsoft.Json;
using System.Collections.ObjectModel;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    public class AllRepairCompaniesViewModel : BindableObject   
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<RepairCompany> _repaircompanies;

        public ObservableCollection<RepairCompany> RepairCompanies
        {
            get => _repaircompanies;
            set
            {
                _repaircompanies = value;
                OnPropertyChanged();
            }
        }

        public Command GeRepairCompaniesCommand { get; }

        public AllRepairCompaniesViewModel()
        {
            Console.WriteLine("[AllRepairCompaniesViewModel] [==============] Constructor");
        }

       public async Task LoadDataAsync()
        {
            Console.WriteLine("[AllRepairCompaniesViewModel] [LoadDataAsync ][==============] ");
            var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/users");
            Console.WriteLine("[AllRepairCompaniesViewModel] [LoadDataAsync] [==============] reponse: " + response);
            var content = await response.Content.ReadAsStringAsync();
            RepairCompanies = JsonConvert.DeserializeObject<ObservableCollection<RepairCompany>>(content);
        }
    }
}
