using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep2Page : ContentPage
{
    private readonly ClaimFormStep2ViewModel _viewModel;
    public ClaimFormStep2Page(ClaimFormStep2ViewModel vm)

    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [ClaimFormStep2ViewModel] viewmodel injected");
        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
        LoadLocationAsync();
    }

    // Show current geo location of the user, with span of 5 miles
    private async void LoadLocationAsync()
    {
        (double currentLatitude, double currentLongitude) = await _viewModel.GetCurrentLocation();
        Device.BeginInvokeOnMainThread(() =>
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(currentLatitude, currentLongitude), Distance.FromMiles(5)));
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        // Convert latitude and longitude to an address
        double latitude = e.Location.Latitude;
        double longitude = e.Location.Longitude;
        Dictionary<string, string> addressData = await _viewModel.GetGeocodeReverseData(latitude, longitude);
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] geocodeData " + addressData);

        if (addressData != null)
        {
            // Read result from Dictionary
            string placeStreet = addressData["Thoroughfare"];
            string placeHouseNumber = addressData["SubThoroughfare"];
            string placePostalCode = addressData["PostalCode"];
            string placeCity = addressData["Locality"];
            string placeCountryName = addressData["CountryName"];

            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] placeStreet " + placeStreet);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] placeHouseNumber " + placeHouseNumber);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] placePostalCode " + placePostalCode);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] placeCity " + placeCity);
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] placeCountryName " + placeCountryName);

            _viewModel.ClaimForm.Street = placeStreet;
            _viewModel.ClaimForm.Suite = placeHouseNumber;
            _viewModel.ClaimForm.ZipCode = placePostalCode;
            _viewModel.ClaimForm.City = placeCity;
        }
    }
}