using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels;

[QueryProperty(nameof(ClaimForm), "ClaimForm")]
public partial class ClaimFormStep4ViewModel : BaseViewModel   
{
    private ClaimForm _claimForm;

    INavigationService navigationService;
    public ClaimFormStep4ViewModel(INavigationService navigationService) 
    {
        this.navigationService = navigationService;
    }

    [ObservableProperty]
    ClaimForm claimForm;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    [RelayCommand]
    async Task Next()
    {
        await Shell.Current.GoToAsync(nameof(ClaimFormStep5Page), true, new Dictionary<string, object>
    {
        {nameof(ClaimForm), ClaimForm}
    });
    }

    [RelayCommand]
    async Task Previous() => 
        await navigationService.GoBackAsync();
}
