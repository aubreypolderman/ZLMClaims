using CommunityToolkit.Mvvm.ComponentModel;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(Contract), "Contract")]
    public partial class ContractViewModel : ObservableObject   
    {
        private Contract _contract;

        public ContractViewModel() { }

        [ObservableProperty]
        Contract contract;

    }

}
