using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(Contract), "Contract")]
    public partial class ContractViewModel : BaseViewModel   
    {
        private Contract _contract;
        INavigationService navigationService;
        public ContractViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        [ObservableProperty]
        Contract contract;

        [RelayCommand]
        async Task Claim() =>
         await navigationService.GoToAsync(nameof(ClaimFormStep1Page));

    }

}
