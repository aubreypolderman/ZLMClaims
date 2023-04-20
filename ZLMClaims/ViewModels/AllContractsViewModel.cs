using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    public partial class AllContractsViewModel : BaseViewModel   
    {
       
        private ObservableCollection<Contract> _contracts;

        public ICommand SelectContractCommand { get; }
        INavigationService navigationService;
        IContractService contractService;
        IDialogService dialogService;
        IConnectivity connectivity;

        // Todo activate so one can refresh the page by pulling doin
        //[ObservableProperty]
        //bool isBusy;

        public ObservableCollection<Contract> Contracts { get; private set; } = new();

        public AllContractsViewModel(INavigationService navigationService, IContractService contractService, IDialogService dialogService, IConnectivity connectivity)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [constructor] Navigation and IContractService injected");
            this.navigationService = navigationService;
            this.contractService = contractService;            
            this.dialogService = dialogService;
            this.connectivity = connectivity;

            GetAllContracts();
            
        }

        public async Task GetAllContracts()
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [GetAllContracts]");
            var id = 1;

           // if (IsLoading) return;
            try
            {
                //    IsBusy = true;
                await dialogService.DisplayAlertAsync("Succes", "Loading list of contracts", "OK");
                if (Contracts.Any()) Contracts.Clear();
                
                var contracts = await contractService.GetAllContractsByPersonIdAsync(id);
                Console.WriteLine("[..............] [AllContractsViewModel] [GetAllContracts] contractService invoked for personId " + id);
                foreach (var contract in contracts)
                {
                    Console.WriteLine("[..............] [AllContractsViewModel] [GetAllContracts] contract:" + contract.Product);
                    Contracts.Add(contract);
                }

            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Unable to get Contracts: {ex.Message}");
                // await Shell.Current.DisplayAlert("Error","Failed to retrieve list of Contracts","OK");
                await dialogService.DisplayAlertAsync("Error", "Failed to retrieve list of Contracts", "OK");
            }
            finally
            {
                //IsBusy = false;
                //isRefreshing = false;
            }
            
        }

    }
}
