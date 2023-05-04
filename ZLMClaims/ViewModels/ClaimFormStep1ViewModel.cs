using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels
{
    [QueryProperty(nameof(Claim), "Claim")]
    public partial class ClaimFormStep1ViewModel : BaseViewModel   
    {
        private Claim _claim;
        private string _selectedOption;

        public List<string> Options { get; } = new List<string>
        {
            "Aanrijding met een vast object",
            "Aanrijding zonder tegenpartij",
            "Diefstal of inbraak",
            "Ruitschade",
            "Andere oorzaak",
            "StartingDate"
        };

        public string SelectedOption
        {
            get => _selectedOption;
            set => SetProperty(ref _selectedOption, value);
        }

        // Maximum date can't be beyond current date, and Minimum date is current date - 3 months. 
        public DateTime MaxDate => DateTime.Now;
        public DateTime MinDate => DateTime.Now.AddMonths(-3);

        INavigationService navigationService;
        ILocalClaimService localClaimService;
        public ClaimFormStep1ViewModel(INavigationService navigationService, ILocalClaimService localClaimService) 
        {
            Console.WriteLine("[..............] [ClaimFormStep1ViewModel] [constructor] Navigation injected");
            this.navigationService = navigationService;
            this.localClaimService = localClaimService;
        }

        [ObservableProperty]
        Claim claim;

        [RelayCommand]
        async Task Next() {
            localClaimService.SaveClaim(claim);
            await navigationService.GoToAsync(nameof(ClaimFormStep5Page), true, new Dictionary<string, object>
            //await Shell.Current.GoToAsync(nameof(ClaimFormStep5Page), true, new Dictionary<string, object>
            {
                {nameof(Claim), claim}
            });
        }

        [RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
