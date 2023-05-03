using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(Claim), "Claim")]
    public partial class ClaimFormStep5ViewModel : BaseViewModel   
    {
        private Claim _claim;

        INavigationService navigationService;
        //readonly ILocalClaimService localClaimService;

        public ClaimFormStep5ViewModel(INavigationService navigationService) 
        {
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [constructor] Navigation injected");
            //Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [constructor] WhatIsDamaged" + claim.QWhatIsDamaged);
            //Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [constructor] Street" + claim.AccidentAddress.Street);
            this.navigationService = navigationService;
            //this.localClaimService = localClaimService;
        }

        [ObservableProperty]
        Claim claim;

        public ObservableCollection<Claim> Claims { get; private set; } = new();

        [RelayCommand]
         async Task Send() =>
             await navigationService.GoBackAsync();
        //   await localClaimService.SaveClaim(Claim);


        [RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
