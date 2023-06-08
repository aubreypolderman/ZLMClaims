﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels;

[QueryProperty(nameof(ClaimForm), "ClaimForm")]
public partial class ClaimFormStep1ViewModel : BaseViewModel
{
    private ClaimForm _claimForm;

    public List<string> Options { get; } = new List<string>
    {
        "Aanrijding met een vast object",
        "Aanrijding zonder tegenpartij",
        "Diefstal of inbraak",
        "Ruitschade",
        "Andere oorzaak"
    };

    // Maximum date can't be beyond current date, and Minimum date is current date - 3 months. 
    public DateTime MaxDate => DateTime.Now;
    public DateTime MinDate => DateTime.Now.AddMonths(-3);

    INavigationService navigationService;
    IContractService contractService;
    IUserService userService;
    public ClaimFormStep1ViewModel(INavigationService navigationService, IContractService contractService, IUserService userService)
    {
        this.navigationService = navigationService;
        this.contractService = contractService;
        this.userService = userService; 
    }

    [ObservableProperty]
    ClaimForm claimForm;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    [RelayCommand]
    async Task Next()
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [Next] Contract id " + ClaimForm.Contract.Id);
        // Saving the contractId
        Preferences.Default.Set("contractId", ClaimForm.Contract.Id);
        await Shell.Current.GoToAsync(nameof(ClaimFormStep2Page), true, new Dictionary<string, object>
        {
            {nameof(ClaimForm), ClaimForm}
        });
    }

    [RelayCommand]
    async Task Previous() =>
        await navigationService.GoBackAsync();

}