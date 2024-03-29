﻿using CommunityToolkit.Mvvm.ComponentModel;
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
            // retrieve the userid from the preference set        
            int userId = Preferences.Default.Get("userId", -1);

            try
            {
                if (Contracts.Any()) Contracts.Clear();
                
                var contracts = await contractService.GetAllContractsByPersonIdAsync(userId);
                foreach (var contract in contracts)
                {
                    Contracts.Add(contract);
                }

            }
            catch (Exception ex) 
            {
                await dialogService.DisplayAlertAsync("Error", "Failed to retrieve list of Contracts", "OK");
            }            
        }

    }
}
