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
    public partial class ClaimFormStep4ViewModel : BaseViewModel   
    {
        private Claim _claim;



        INavigationService navigationService;
        public ClaimFormStep4ViewModel(INavigationService navigationService) 
        {
            Console.WriteLine("[..............] [ClaimFormStep4ViewModel] [constructor] Navigation injected");
            this.navigationService = navigationService;
        }

        [ObservableProperty]
        Claim claim;

        public ObservableCollection<Claim> Claims { get; private set; } = new();

        [RelayCommand]
        async Task Next() =>
            await navigationService.GoToAsync(nameof(ClaimFormStep5Page));

        [RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
