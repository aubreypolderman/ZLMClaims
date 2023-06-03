using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZLMClaims.Models;
using ZLMClaims.Services;
using ZLMClaims.Views;

namespace ZLMClaims.ViewModels;

[QueryProperty(nameof(ClaimForm), "ClaimForm")]
public partial class ClaimFormStep2ViewModel : BaseViewModel
{
    private ClaimForm _claimForm;
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    readonly ObservableCollection<Position> _positions;
    public IEnumerable Positions => _positions;

    INavigationService navigationService;
    IDialogService dialogService;
    public ClaimFormStep2ViewModel(INavigationService navigationService, IDialogService dialogService)
    {
        this.navigationService = navigationService;
        this.dialogService = dialogService;
    }

    [ObservableProperty]
    ClaimForm claimForm;

    public ObservableCollection<ClaimForm> ClaimForms { get; private set; } = new();

    [RelayCommand]
    async Task Next()
    {
        await Shell.Current.GoToAsync(nameof(ClaimFormStep3Page), true, new Dictionary<string, object>
    {
        {nameof(ClaimForm), ClaimForm}
    });
    }

    [RelayCommand]
    async Task Previous() =>
        await navigationService.GoBackAsync();

    //public async Task<string> GetGeocodeReverseData(double latitude, double longitude)
    public async Task<Dictionary<string, string>> GetGeocodeReverseData(double latitude, double longitude)
    {
        IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

        Placemark placemark = placemarks?.FirstOrDefault();

        if (placemark != null)
        {
            Dictionary<string, string> addressData = new Dictionary<string, string>
            {
                { "AdminArea", placemark.AdminArea },
                { "CountryCode", placemark.CountryCode },
                { "CountryName", placemark.CountryName },
                { "FeatureName", placemark.FeatureName },
                { "Locality", placemark.Locality },
                { "PostalCode", placemark.PostalCode },
                { "SubAdminArea", placemark.SubAdminArea },
                { "SubLocality", placemark.SubLocality },
                { "SubThoroughfare", placemark.SubThoroughfare },
                { "Thoroughfare", placemark.Thoroughfare }
            };
            return addressData;
        }

        return null;
    }

    public string CalculateDistanceInKm(double currentLongitude, double currentLatitude, double selectedLongitude, double selectedLatitude)
    {
        // calculate distance in kilometers between current location and location of selected repaircompany
        var currentLocation = new Location(currentLatitude, currentLongitude);
        var selectedLocation = new Location(selectedLatitude, selectedLongitude);
        double distance = Location.CalculateDistance(currentLocation, selectedLocation, DistanceUnits.Kilometers);
        string formattedDistance = distance.ToString("0.0");

        return formattedDistance;
    }

    public async Task<(double Latitude, double Longitude)> GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            {
                return (location.Latitude, location.Longitude);
            }
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetCurrentLocation] " + "Failed to retrieve current location. ERROR message: " + ex.Message);
            await dialogService.DisplayAlertAsync("Error", "Failed to retrieve current location", "OK");
        }
        finally
        {
            _isCheckingLocation = false;
        }

        // Return default values if location cannot be obtained
        return (0, 0);
    }


    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}
