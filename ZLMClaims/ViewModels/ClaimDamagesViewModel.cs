using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(Contract), "Contract")]
    public partial class ClaimDamagesViewModel : ObservableObject   
    {
        private Contract _contract;
        public ICommand SelectContractCommand { get; }
        private INavigation navigation;

        public ClaimDamagesViewModel() { }

        [ObservableProperty]
        Contract contract;

    }

}
