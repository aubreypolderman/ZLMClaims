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
        private Command<object> tapCommand;
        public ICommand SelectContractCommand { get; }
        INavigationService navigationService;
        IContractService contractService;

        // Todo activate so one can refresh the page by pulling doin
        //[ObservableProperty]
        //bool isBusy;

        public ObservableCollection<Contract> Contracts
        {
            get => _contracts;
        }

        public AllContractsViewModel(INavigationService navigationService, IContractService contractService)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [constructor] Navigation and IContractService injected");
            this.navigationService = navigationService;
            this.contractService = contractService;

            LoadDataAsync();
           
            SelectContractCommand = new AsyncRelayCommand<ViewModels.AllContractsViewModel>(SelectContractAsync);
        }

        private async Task SelectContractAsync(ViewModels.AllContractsViewModel contract)
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [SelectContractAsync] contract:" + contract);
        }
        

       public async Task LoadDataAsync()
        {
            Console.WriteLine("[..............] [AllContractsViewModel] [LoadDataAsync]");
            var id = 1;

           // if (IsLoading) return;
            try
            {
            //    IsBusy = true;
                if (Contracts.Any()) Contracts.Clear();
                
                var contracts = await contractService.GetAllContractsByPersonIdAsync(id);
                Console.WriteLine("[..............] [AllContractsViewModel] [LoadDataAsync] contractService invoked for personId " + id);
                foreach (var contract in contracts)
                {
                    Console.WriteLine("[..............] [AllContractsViewModel] [LoadDataAsync] contract:" + contract.Product);
                    Contracts.Add(contract);
                }

            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Unable to get Contracts: {ex.Message}");
                await Shell.Current.DisplayAlert("Error","Failed to retrieve list of Contracts","OK");
            }
            finally
            {
                //IsBusy = false;
                //isRefreshing = false;
            }
            
        }

    }
}
