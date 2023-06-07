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
        private bool isUpdate = false;
        INavigationService navigationService;
        IUserService userService;
        IContractService contractService;
        public ContractViewModel(INavigationService navigationService, IUserService userService, IContractService contractService)
        {
            this.navigationService = navigationService;
            this.userService = userService;
            this.contractService = contractService;
        }

        [ObservableProperty]
        Contract contract;        

    }

}
