using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;
using System.ComponentModel;

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
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(currentLatitude, currentLongitude), Distance.FromMiles(0.5)));
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

            // Voeg een nieuwe pin toe aan de kaart op de geklikte locatie
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] invoke setPinAcidentLocation");
            _viewModel.setPinAccidentLocation();
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] before map.Pins.Add");
            map.Pins.Add(new Pin
            {
                Location = new Location(e.Location.Latitude, e.Location.Longitude),
                Label = $"{placeStreet} {placeHouseNumber} {placePostalCode} {placeCity}",
                Type = PinType.Generic
            });
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] na map.Pins.Add");
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] update viewModel with placemark");
            _viewModel.ClaimForm.Street = placeStreet;
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] viewmodel updated with placeStreet");
            _viewModel.ClaimForm.Suite = placeHouseNumber;
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] viewmodel updated with placeHouseNumber");
            _viewModel.ClaimForm.ZipCode = placePostalCode;
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] viewmodel updated with placePostalCode");
            _viewModel.ClaimForm.City = placeCity;
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [OnMapClicked] viewmodel updated with placeCity");
        }
    }
}