using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Devices.Sensors;
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
        _positions = new ObservableCollection<Position>();
        // setPinAccidentLocation();
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
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetGeocodeReverseData] " + "latitude: " + latitude + " longitude: " + longitude);
        IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetGeocodeReverseData] " + "plaxemarks: " + placemarks );
        Placemark placemark = placemarks?.FirstOrDefault();
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetGeocodeReverseData] " + "placemark: " + placemark);

        if (placemark != null)
        {
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetGeocodeReverseData] " + "placemark not null");
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
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetGeocodeReverseData] " + "return addressData: " + addressData);
            return addressData;
        }
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetGeocodeReverseData] " + "return null" );
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
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetCurrentLocation] " + "Return latitude " + location.Latitude + " and longitude " + location.Longitude);
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
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetCurrentLocation] " + "No location obtained. Return 0,0" );
        return (0, 0);
    }


    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }

    public void setPinAccidentLocation()
    {
        
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [setPinAccidentLocation] Start");
        double latitude = (double)(ClaimForm?.Latitude);
        double longitude = (double)(ClaimForm?.Longitude);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [setPinAccidentLocation] Latitude " + latitude);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [setPinAccidentLocation] Longitude " + longitude);
        var position = new Position(ClaimForm.Street, ClaimForm.Suite, new Location(latitude, longitude));
        _positions.Add(position);
        
    }

    public async Task GetAcciddentAddressLocation()
    {        
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetAcciddentAddressLocation] Start");
        var position = new Position(ClaimForm.Street + " " + ClaimForm.Suite, ClaimForm.ZipCode + " " + ClaimForm.City, new Location((double)ClaimForm.Latitude, (double)ClaimForm.Longitude));
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetAcciddentAddressLocation] Position " + position + " set for address " + ClaimForm.Street + " " + ClaimForm.Suite);
        _positions.Add(position);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2ViewModel] [GetAcciddentAddressLocation] Position " + position + " added");
    }
}
