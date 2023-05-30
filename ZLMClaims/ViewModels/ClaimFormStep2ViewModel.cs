using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels;

[QueryProperty(nameof(Claim), "Claim")]
public partial class ClaimFormStep2ViewModel : BaseViewModel
{
    private Claim _claim;
    readonly ObservableCollection<Position> _positions;
    public IEnumerable Positions => _positions;

    INavigationService navigationService;
    ILocalClaimService localClaimService;
    public ClaimFormStep2ViewModel(INavigationService navigationService, ILocalClaimService localClaimService)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [constructor] Navigation injected");
        this.navigationService = navigationService;
        this.localClaimService = localClaimService;
    }

    [ObservableProperty]
    Claim claim;

    public ObservableCollection<Claim> Claims { get; private set; } = new();

    [RelayCommand]
    //async Task Next() =>
    //    await navigationService.GoToAsync(nameof(ClaimFormStep2Page));
    async Task Next()
    {
        // localClaimService.SaveClaim(_claim);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [Next] Cause of damange => " + Claim?.QCauseOfDamage);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [Next] DateOfOccurence => " + Claim?.DateOfOccurence);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [Next] What happened => " + Claim?.QWhatHappened);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [Next] What is damaged => " + Claim?.QWhatIsDamaged);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [Next] Where's the damage => " + Claim?.QWhereDamaged);
        await Shell.Current.GoToAsync(nameof(ClaimFormStep3Page), true, new Dictionary<string, object>
    {
        {nameof(Claim), Claim}
    });
    }

    [RelayCommand]
    async Task Previous() =>
        await navigationService.GoBackAsync();

    public async Task AddPin(double latitude, double longitude, string location, string address, string label)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [AddPin] address " + address + " label " + label);
        var position = new Position(address, label, new Location(latitude, longitude));
        _positions.Add(position);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [AddPin] Pin toegevoegd " + position);
    }
}
