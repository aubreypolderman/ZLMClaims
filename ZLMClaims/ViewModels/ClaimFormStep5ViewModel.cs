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
    IDialogService dialogService;
    readonly IClaimFormService claimFormService;

    public ClaimFormStep5ViewModel(INavigationService navigationService, IClaimFormService claimFormService, IDialogService dialogService) 
    {
        this.navigationService = navigationService;
        this.claimFormService = claimFormService;
        this.dialogService = dialogService;
    }

    [ObservableProperty]
    ClaimForm claimForm;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    [RelayCommand]
    async Task Send()
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] QWhatHappened => " + ClaimForm.QWhatHappened);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] QWhereDamaged => " + ClaimForm.QWhereDamaged);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] Product => " + ClaimForm.Contract.Product);
        
        // Make the claimform object
        var user = new User
        {
            Id = 1,
            Name = "Aubrey Polderman",
            Username = "aubreypolderman",
            Email = "aubreypolderman@gmail.com",
            Phone = "06-12345678",
            Street = "Cirkel",
            Housenumber = "63",
            City = "Vlissingen",
            Zipcode = "4384DS",
            Latitude = 51.461684899386995,
            Longitude = 3.5559567820729203
        };
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] User instantiated");

        var contract = new Contract
        {
            Id = 1, // ClaimForm.ContractId, -> werkt niet op de een of andere manier
            Product = ClaimForm.Contract.Product,
            Make = ClaimForm.Contract.Make,
            Model = ClaimForm.Contract.Model,
            LicensePlate = ClaimForm.Contract.LicensePlate,
            DamageFreeYears = ClaimForm.Contract.DamageFreeYears,
            StartingDate = ClaimForm.Contract.StartingDate,
            EndDate = ClaimForm.Contract.EndDate,
            AnnualPolicyPremium = ClaimForm.Contract.AnnualPolicyPremium,
            UserId = 1,
            User = user
        };
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] Contract instantiated");

        var claimForm = new ClaimForm
        {
            ContractId = 1, //ClaimForm.ContractId,
            DateOfOccurence = ClaimForm.DateOfOccurence,
            QCauseOfDamage = ClaimForm.QCauseOfDamage,
            QWhatHappened = ClaimForm.QWhatHappened,
            QWhereDamaged = ClaimForm.QWhereDamaged,
            QWhatIsDamaged = ClaimForm.QWhatIsDamaged,
            Image1 = "img5001.jpg",
            Image2 = "img5002.jpg",
            Street = ClaimForm.Street,
            Suite = ClaimForm.Suite,
            City = ClaimForm.City,
            ZipCode = ClaimForm.ZipCode,
            Latitude = ClaimForm.Latitude,
            Longitude = ClaimForm.Longitude,
            Contract = contract
        };
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] ClaimForm instantiated");

        // Save the claimform        
        var response = await claimFormService.CreateClaimFormAsync(claimForm);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep5ViewModel] [Next] Result reponse => " + response);

        if (response.IsSuccessStatusCode)
        {
            await dialogService.DisplayAlertAsync("Succes", "Claimform has been succesfully sent to ZLM", "OK");
        }
        else
        {
            await dialogService.DisplayAlertAsync("Error", "Error when sending ClaimForm to ZLM: " + response.ReasonPhrase, "OK");
        }

    }

    [RelayCommand]
    async Task Previous() => 
        await navigationService.GoBackAsync();
}
