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
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [constructor] Navigation injected");
            this.navigationService = navigationService;
        }

        [ObservableProperty]
        Claim claim;

        public ObservableCollection<Claim> Claims { get; private set; } = new();

        [RelayCommand]
        async Task Next()
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] Cause of damange => " + Claim?.QCauseOfDamage);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] DateOfOccurence => " + Claim?.DateOfOccurence);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] What happened => " + Claim?.QWhatHappened);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] What is damaged => " + Claim?.QWhatIsDamaged);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] Where's the damage => " + Claim?.QWhereDamaged);

            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] Street => " + Claim?.AccidentAddress?.Street);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] Suite => " + Claim?.AccidentAddress?.Suite);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] Zipcode => " + Claim?.AccidentAddress?.Zipcode);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] City => " + Claim?.AccidentAddress?.City);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] Licenseplate => " + Claim?.Contract?.LicensePlate);
            await Shell.Current.GoToAsync(nameof(ClaimFormStep5Page), true, new Dictionary<string, object>
        {
            {nameof(Claim), Claim}
        });
        }

        [RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
