using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels;

[QueryProperty(nameof(ClaimForm), "ClaimForm")]
public partial class ClaimFormStep5ViewModel : BaseViewModel   
{
    private ClaimForm _claimForm;

    INavigationService navigationService;
    readonly IClaimFormService claimFormService;

    public ClaimFormStep5ViewModel(INavigationService navigationService, IClaimFormService claimFormService) 
    {
        this.navigationService = navigationService;
        this.claimFormService = claimFormService;
    }

    [ObservableProperty]
    ClaimForm claimForm;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    /*
    [RelayCommand]
     async Task Send() =>
         //await navigationService.GoBackAsync();
       await localClaimService.SaveClaim(Claim);
    */

    [RelayCommand]
    async Task Send()
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [Next] Claim => " + ClaimForm?.ToString());
       // await claimFormService.
   
    }

    [RelayCommand]
    async Task Previous() => 
        await navigationService.GoBackAsync();
}
