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
            this.navigationService = navigationService;
            this.contractService = contractService;            
            this.dialogService = dialogService;
            this.connectivity = connectivity;
        }

        public async Task GetAllContracts()
        {
            // Get User by retrieving the userid from the SecureStorage 
            string userIdString = await SecureStorage.GetAsync("userId");
            int userId = userIdString != null ? int.Parse(userIdString) : -1;
            Debug.WriteLine(DateTime.Now + "[..........] [AllContractsViewModel] [GetAllContracts] userId = " + userId);

            try
            {
                if (Contracts.Any()) Contracts.Clear();
                
                var contracts = await contractService.GetAllContractsByPersonIdAsync(userId);
                Debug.WriteLine(DateTime.Now + "[..........] [AllContractsViewModel] [GetAllContracts] Nr. of contracts received: " + contracts.Count());
                foreach (var contract in contracts)
                {
                    Contracts.Add(contract);
                }

            }
            catch (Exception ex) 
            {
                Debug.WriteLine(DateTime.Now + "[..........] [AllContractsViewModel] [GetAllContracts] Exception = " + ex);
                Debug.WriteLine(DateTime.Now + "[..........] [AllContractsViewModel] [GetAllContracts] Exception message = " + ex.Message);
                await dialogService.DisplayAlertAsync("Error", "Failed to retrieve list of Contracts", "OK");
            }            
        }

    }
}
