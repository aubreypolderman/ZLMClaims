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
        BindingContext = vm;
        _viewModel = vm;        
        InitializeComponent();

    }

    // Show current geo location of the user, with span of 5 miles
    private async void LoadLocationAsync()
    {
        (double currentLatitude, double currentLongitude) = await _viewModel.GetCurrentLocation();
        Action action = () =>
                {
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(currentLatitude, currentLongitude), Distance.FromMiles(0.5)));
                };
        Device.BeginInvokeOnMainThread(action);
    }

    private async void AddPinToAccidentLocation()
    {
        await _viewModel.GetAcciddentAddressLocation();       
        map.Pins.Add(new Pin
        {
            Location = new Location((double)_viewModel.ClaimForm.Latitude, (double)_viewModel.ClaimForm.Longitude),
            Label = $"{_viewModel.ClaimForm.Street} {_viewModel.ClaimForm.Suite} {_viewModel.ClaimForm.ZipCode} {_viewModel.ClaimForm.City}",
            Type = PinType.Generic
        });

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        LoadLocationAsync();
        AddPinToAccidentLocation();
    }

    async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        // Convert latitude and longitude to an address
        double latitude = e.Location.Latitude;
        double longitude = e.Location.Longitude;
        Dictionary<string, string> addressData = await _viewModel.GetGeocodeReverseData(latitude, longitude);

        if (addressData != null)
        {
            // Read result from Dictionary
            string placeStreet = addressData["Thoroughfare"];
            string placeHouseNumber = addressData["SubThoroughfare"];
            string placePostalCode = addressData["PostalCode"];
            string placeCity = addressData["Locality"];
            string placeCountryName = addressData["CountryName"];

            // Voeg een nieuwe pin toe aan de kaart op de geklikte locatie
            _viewModel.setPinAccidentLocation(latitude, longitude);

            map.Pins.Add(new Pin
            {
                Location = new Location(e.Location.Latitude, e.Location.Longitude),
                Label = $"{placeStreet} {placeHouseNumber} {placePostalCode} {placeCity}",
                Type = PinType.Generic
            });

            _viewModel.ClaimForm.Street = placeStreet;
            _viewModel.ClaimForm.Suite = placeHouseNumber;
            _viewModel.ClaimForm.ZipCode = placePostalCode;            
            _viewModel.ClaimForm.City = placeCity;
        }
    }
}