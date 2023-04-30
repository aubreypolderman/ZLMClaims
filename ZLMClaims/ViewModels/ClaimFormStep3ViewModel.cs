using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(Claim), "Claim")]
    public partial class ClaimFormStep3ViewModel : BaseViewModel   
    {
        private Claim _claim;

        INavigationService navigationService;
        public ClaimFormStep3ViewModel(INavigationService navigationService) 
        {
            Console.WriteLine("[..............] [ClaimFormStep3ViewModel] [constructor] Navigation injected");
            this.navigationService = navigationService;
        }

        [ObservableProperty]
        Claim claim;



        [RelayCommand]
        async Task Next() =>
            await navigationService.GoToAsync(nameof(ClaimFormStep4Page));

        [RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
