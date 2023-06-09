using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels;

[QueryProperty(nameof(ClaimForm), "ClaimForm")]
public partial class ClaimFormStep1ViewModel : BaseViewModel
{
    private ClaimForm _claimForm;

    public List<string> Options { get; } = new List<string>
    {
        "Aanrijding met een vast object",
        "Aanrijding zonder tegenpartij",
        "Diefstal of inbraak",
        "Ruitschade",
        "Andere oorzaak"
    };

    // Maximum date can't be beyond current date, and Minimum date is current date - 3 months. 
    public DateTime MaxDate => DateTime.Now;
    public DateTime MinDate => DateTime.Now.AddMonths(-3);

    INavigationService navigationService;
    IContractService contractService;
    IUserService userService;
    public ClaimFormStep1ViewModel(INavigationService navigationService, IContractService contractService, IUserService userService)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [Constructor] Start");
        this.navigationService = navigationService;
        this.contractService = contractService;
        this.userService = userService;
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [Constructor] Before ClaimFormStep1ViewModel_PropertyChanged");
        PropertyChanged += ClaimFormStep1ViewModel_PropertyChanged;
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [Constructor] After ClaimFormStep1ViewModel_PropertyChanged");

    }
    private void ClaimFormStep1ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [ClaimFormStep1ViewModel_PropertyChanged] Start");
        if (e.PropertyName == nameof(ClaimForm))
        {
            if (ClaimForm != null && ClaimForm.DateOfOccurence.HasValue)
            {
                SelectedTime = ClaimForm.DateOfOccurence.Value.TimeOfDay;
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [ClaimFormStep1ViewModel_PropertyChanged] SelectedTime: " + SelectedTime);
            }
        }
    }
    [ObservableProperty]
    ClaimForm claimForm;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    private TimeSpan selectedTime;
    public TimeSpan SelectedTime
    {
        get { return selectedTime; }
        set { SetProperty(ref selectedTime, value);
        }
    }

    [RelayCommand]
    async Task Next()
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [Next] Contract id " + ClaimForm.Contract.Id);
        // Saving the contractId
        Preferences.Default.Set("contractId", ClaimForm.Contract.Id);
        DateTime selectedDateTime = ClaimForm.DateOfOccurence.Value.Date + SelectedTime;        
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [Next] combinedDateTime: " + selectedDateTime);

        // Saving combined DateTime values in DateOfOccurrence
        ClaimForm.DateOfOccurence = selectedDateTime;
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep1ViewModel] [Next] ClaimForm.DateOfOccurence: " + ClaimForm.DateOfOccurence);

        await Shell.Current.GoToAsync(nameof(ClaimFormStep2Page), true, new Dictionary<string, object>
        {
            {nameof(ClaimForm), ClaimForm}
        });
    }

    [RelayCommand]
    async Task Previous() =>
        await navigationService.GoBackAsync();

}