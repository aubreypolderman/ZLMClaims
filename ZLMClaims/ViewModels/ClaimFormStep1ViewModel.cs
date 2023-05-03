using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
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
            "Andere oorzaal",
            "StartingDate"
        };

        public string SelectedOption
        {
            get => _selectedOption;
            set => SetProperty(ref _selectedOption, value);
        }

        INavigationService navigationService;
        public ClaimFormStep1ViewModel(INavigationService navigationService) 
        {
            Console.WriteLine("[..............] [ClaimFormStep1ViewModel] [constructor] Navigation injected");
            this.navigationService = navigationService;
        }

        [ObservableProperty]
        Claim claim;



        [RelayCommand]
        //async Task Next() =>
        //    await navigationService.GoToAsync(nameof(ClaimFormStep2Page));
        async Task Next() =>
        await Shell.Current.GoToAsync(nameof(ClaimFormStep5Page), true, new Dictionary<string, object>
        {
            {nameof(Claim), claim}
        });


[RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
