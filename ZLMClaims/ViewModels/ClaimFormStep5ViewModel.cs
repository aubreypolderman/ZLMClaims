using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] ClaimId => " + ClaimForm?.Id);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] DateOfOccurence => " + ClaimForm?.DateOfOccurence);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] QWhatHappened => " + ClaimForm?.QWhatHappened);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] QWhereDamaged => " + ClaimForm?.QWhereDamaged);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] QCauseOfDamage => " + ClaimForm?.QCauseOfDamage);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] Street => " + ClaimForm?.Street);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] Suite => " + ClaimForm?.Suite);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] ZipCode => " + ClaimForm?.ZipCode);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] City => " + ClaimForm?.City);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] Latitude => " + ClaimForm?.Latitude);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Claim] Longitude => " + ClaimForm.Longitude);

        // Get User
        int userId = Preferences.Default.Get("userId", -1);
        var user = await userService.GetUserByIdAsync(userId);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Id: " + user.Id);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Name: " + user.Name);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Username " + user.Username);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Email: " + user.Email);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Street: " + user.Street);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Housenumber: " + user.Housenumber);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] City: " + user.City);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Phone: " + user.Phone);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Latitude " + user.Latitude);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [User] Longitude: " + user.Longitude);

        // Get Contract
        int contractId = Preferences.Default.Get("contractId", -1);
        var contract = await contractService.GetContractByIdAsync(contractId);

        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] Id: " + contract.Id);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] Product: " + contract.Product);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] Make: " + contract.Make);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] Model: " + contract.Model);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] AnnualPolicyPremium: " + contract.AnnualPolicyPremium);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] DamageFreeYears: " + contract.DamageFreeYears);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] StartingDate: " + contract.StartingDate);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] EndDate: " + contract.EndDate);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] LicensePlate: " + contract.LicensePlate);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Contract] UserId: " + contract.UserId);

        var contractClaim = new Contract
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
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] ClaimForm instantiated for claim with id = " + ClaimForm.Id);

        // Save the claimform        
        bool isSuccess = await claimFormService.SaveClaimFormAsync(claimForm);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] Result saveClaimFormAsync => " + isSuccess);

        if (isSuccess)
        {
            await dialogService.DisplayAlertAsync("Success", "Claim form has been successfully sent to ZLM", "OK");
            await Shell.Current.GoToAsync(nameof(AllClaimsPage));
        }
        else
        {
            await dialogService.DisplayAlertAsync("Error", "Error when sending ClaimForm to ZLM. You will be redirected to Home", "OK");
        }

    }

    [RelayCommand]
    async Task Previous() => 
        await navigationService.GoBackAsync();
}
