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

        public ObservableCollection<Claim> Claims { get; private set; } = new();

        [RelayCommand]
        //async Task Next() =>
        //    await navigationService.GoToAsync(nameof(ClaimFormStep2Page));
        async Task Next() =>
        await Shell.Current.GoToAsync(nameof(ClaimFormStep4Page), true, new Dictionary<string, object>
        {
            {nameof(Claim), claim}
        });

        [RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
