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
    public partial class ClaimFormStep5ViewModel : BaseViewModel   
    {
        private Claim _claim;

        INavigationService navigationService;
        readonly ILocalClaimService localClaimService;

        public ClaimFormStep5ViewModel(INavigationService navigationService, ILocalClaimService localClaimService) 
        {
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [constructor] Navigation and localclaimservice injected");
            this.navigationService = navigationService;
            this.localClaimService = localClaimService;
        }

        [ObservableProperty]
        Claim claim;

        public ObservableCollection<Claim> Claims { get; private set; } = new();

        /*
        [RelayCommand]
         async Task Send() =>
             //await navigationService.GoBackAsync();
           await localClaimService.SaveClaim(Claim);
        */

        [RelayCommand]
        async Task Send()
        {
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [Before Send] Cause of damange => " + Claim?.QCauseOfDamage);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [Before Send] DateOfOccurence => " + Claim?.DateOfOccurence);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [Before Send] What happened => " + Claim?.QWhatHappened);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [Before Send] What is damaged => " + Claim?.QWhatIsDamaged);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [Before Send] Where's the damage => " + Claim?.QWhereDamaged);
            await localClaimService.SaveClaim(Claim);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [After Send] Cause of damange => " + Claim?.QCauseOfDamage);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [After Send] DateOfOccurence => " + Claim?.DateOfOccurence);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [After Send] What happened => " + Claim?.QWhatHappened);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [After Send] What is damaged => " + Claim?.QWhatIsDamaged);
            Console.WriteLine("[..............] [ClaimFormStep5ViewModel] [After Send] Where's the damage => " + Claim?.QWhereDamaged);
        }

        [RelayCommand]
        async Task Previous() => 
            await navigationService.GoBackAsync();
    }

}
