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

    [ObservableProperty]
    public string imageFileName;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    private string _base64EncodedImage;
    public string Base64EncodedImage
    {
        get { return _base64EncodedImage; }
        set
        {
            _base64EncodedImage = value;
            OnPropertyChanged(nameof(Base64EncodedImage));
        }
    }

    public void SetBase64EncodedImage(string base64EncodedImage)
    {
        Base64EncodedImage = base64EncodedImage;
    }

    [RelayCommand]
    async Task Next()
    {
        ClaimForm.Image1 = ImageFileName; // Update the ClaimForm with the selected cause of damage
        ClaimForm.Image2 = _base64EncodedImage;
        await Shell.Current.GoToAsync(nameof(ClaimFormStep5Page), true, new Dictionary<string, object>
    {
        {nameof(ClaimForm), ClaimForm}
    });
    }

    [RelayCommand]
    async Task Previous() => 
        await navigationService.GoBackAsync();
}
