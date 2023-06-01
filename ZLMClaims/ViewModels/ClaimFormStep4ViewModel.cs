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
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [constructor]");
    }

    [ObservableProperty]
    ClaimForm claimForm;

    [ObservableProperty]
    public string imageFileName;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    [RelayCommand]
    async Task Next()
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [constructor] imageFileName " + imageFileName);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4ViewModel] [constructor] ImageFileName " + ImageFileName);
        ClaimForm.Image1 = ImageFileName; // Update the ClaimForm with the selected cause of damage
        await Shell.Current.GoToAsync(nameof(ClaimFormStep5Page), true, new Dictionary<string, object>
    {
        {nameof(ClaimForm), ClaimForm}
    });
    }

    [RelayCommand]
    async Task Previous() => 
        await navigationService.GoBackAsync();
}
