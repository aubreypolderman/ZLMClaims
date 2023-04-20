using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    public class AllRepairCompaniesViewModel : BaseViewModel   
    {
        private ObservableCollection<RepairCompany> _repaircompanies;

        public ICommand SelectRepairCompanyCommand { get; }
        INavigationService navigationService;
        IRepairCompanyService repairCompanyService;

        public ObservableCollection<RepairCompany> RepairCompanies { get; private set; } = new();

        public AllRepairCompaniesViewModel(INavigationService navigationService, IRepairCompanyService repairCompanyService)
        {
            Console.WriteLine("[..............] [AllRepairCompaniesViewModel] [constructor] INavigation and IRepairCompanyService injected");
            this.navigationService = navigationService;
            this.repairCompanyService = repairCompanyService;

            GetAllRepairCompanies();

            SelectRepairCompanyCommand = new AsyncRelayCommand<AllRepairCompaniesViewModel>(SelectRepairCompanyAsync);
        }

        private async Task SelectRepairCompanyAsync(AllRepairCompaniesViewModel repaircompany)
        {
            // if (repaircompany != null) 
            //     await Shell.Current.GoToAsync($"{nameof(RepairCompanyPage)}?load={repaircompany.Identifier}");
            Console.WriteLine("[SelectRepairCompanyAsync] [==============] repaircompany:" + repaircompany);
        }

        public async Task GetAllRepairCompanies()
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [GetAllRepairCompanies]");

            // if (IsLoading) return;
            try
            {
                //    IsBusy = true;
                if (RepairCompanies.Any()) RepairCompanies.Clear();

                var repaircompanies = await repairCompanyService.GetRepairCompaniesAsync();
                Console.WriteLine("[..............] [AllContractsViewModel] [GetAllRepairCompanies] RepairCompanyService invoked");
                foreach (var repaircompany in repaircompanies)
                {
                    Console.WriteLine("[..............] [AllContractsViewModel] [GetAllRepairCompanies] Repaircompany:" + repaircompany.Name);
                    RepairCompanies.Add(repaircompany);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Repaircompanies: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrieve list of Repaircompanies", "OK");
            }
            finally
            {
                //IsBusy = false;
                //isRefreshing = false;
            }

        }
    }
}
