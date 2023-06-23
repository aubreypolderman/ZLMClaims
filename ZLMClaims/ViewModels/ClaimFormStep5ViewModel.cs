using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Security.Claims;
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
    IDialogService dialogService;
    IUserService userService;
    IContractService contractService;
    readonly IClaimFormService claimFormService;

    public ClaimFormStep5ViewModel(INavigationService navigationService, IClaimFormService claimFormService, IDialogService dialogService, IUserService userService, IContractService contractService) 
    {
        this.navigationService = navigationService;
        this.claimFormService = claimFormService;
        this.dialogService = dialogService;
        this.userService = userService;
        this.contractService = contractService;
    }

    [ObservableProperty]
    ClaimForm claimForm;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    [RelayCommand]
    async Task Send()
    {

        // Get User by retrieving the userid from the SecureStorage 
        string userIdString = await SecureStorage.GetAsync("userId");
        int userId = userIdString != null ? int.Parse(userIdString) : -1;
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep5ViewModel] [Send] userId: " + userId);
        var user = await userService.GetUserByIdAsync(userId);
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep5ViewModel] [Send] user: " + user);

        // Get Contract by retrieving the contractid from the SecureStorage 
        string contractIdString = await SecureStorage.GetAsync("contractId");
        int contractId = contractIdString != null ? int.Parse(contractIdString) : -1;
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep5ViewModel] [Send] contractId: " + contractId);
        var contract = await contractService.GetContractByIdAsync(contractId);
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep5ViewModel] [Send] contract: " + contract);

        var contractClaim = new Models.Contract
        {
            Id = contract.Id, 
            Product = contract.Product,
            Make = contract.Make,
            Model = contract.Model,
            LicensePlate = contract.LicensePlate,
            DamageFreeYears = contract.DamageFreeYears,
            StartingDate = contract.StartingDate,
            EndDate = contract.EndDate,
            AnnualPolicyPremium = contract.AnnualPolicyPremium,
            UserId = userId,
            User = user
        };

        var claimForm = new ClaimForm
        {
            Id = ClaimForm.Id,
            ContractId = contract.Id, //ClaimForm.ContractId,
            DateOfOccurence = ClaimForm.DateOfOccurence,
            QCauseOfDamage = ClaimForm.QCauseOfDamage,
            QWhatHappened = ClaimForm.QWhatHappened,
            QWhereDamaged = ClaimForm.QWhereDamaged,
            QWhatIsDamaged = ClaimForm.QWhatIsDamaged,
            Image1 = ClaimForm.Image1,
            Image2 = ClaimForm.Image2,
            Street = ClaimForm.Street,
            Suite = ClaimForm.Suite,
            City = ClaimForm.City,
            ZipCode = ClaimForm.ZipCode,
            Latitude = ClaimForm.Latitude,
            Longitude = ClaimForm.Longitude,
            Contract = contractClaim
        };

        // Save the claimform        
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep5ViewModel] [Send] claimForm: " + claimForm);
        bool isSuccess = await claimFormService.SaveClaimFormAsync(claimForm);
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep5ViewModel] [Send] isSuccess: " + isSuccess);

        if (isSuccess)
        {
            await Shell.Current.Navigation.PopToRootAsync();
            //await Shell.Current.GoToAsync(nameof(AllClaimsPage));
            await Shell.Current.GoToAsync($"//{nameof(AllClaimsPage)}");

        }
        else
        {
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.GoToAsync(nameof(AllClaimsPage));
            //await Shell.Current.GoToAsync($"//{nameof(AllClaimsPage)}"); 
        }

    }

    [RelayCommand]
    async Task Previous() => 
        await navigationService.GoBackAsync();
}
