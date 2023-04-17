using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    public class AllContractsViewModel : BindableObject   
    {
       
        private ObservableCollection<Contract> _contracts;
        private Command<object> tapCommand;
        public ICommand SelectContractCommand { get; }
        INavigationService navigationService;
        IContractService contractService;

        public ObservableCollection<Contract> Contracts
        {
            get => _contracts;
            set
            {
                _contracts = value;
                OnPropertyChanged();
            }
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
            var contracts = await contractService.GetAllContractsByPersonIdAsync(1);
            Console.WriteLine("[..............] [AllContractsViewModel] [LoadDataAsync] contractService invoked...");
            foreach (var contract in contracts)
            {
                Console.WriteLine("[..............] [AllContractsViewModel] [LoadDataAsync] contract:" + contract.Product);
                Contracts.Add(contract);
            }
        }

    }
}
