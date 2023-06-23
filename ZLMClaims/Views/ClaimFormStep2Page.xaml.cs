using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace ZLMClaims.Views;

public partial class ClaimFormStep2Page : ContentPage
{
    private readonly ClaimFormStep2ViewModel _viewModel;
    private Pin _currentPin;

    public ClaimFormStep2Page(ClaimFormStep2ViewModel vm)

    {
        _viewModel = vm;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    // Show current geo location of the user, with span of 0.5 miles
    private async void LoadLocationAsync()
    {
        await Task.Delay(1000);
        (double currentLatitude, double currentLongitude) = await _viewModel.GetCurrentLocation();
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] LoadLocationAsync: GetCurrentLocation() invoked. currentLatitude=" + currentLatitude + " and currentLongitude=" + currentLongitude);

        if (currentLatitude != 0 && currentLongitude != 0)
        {
            Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] LoadLocationAsync: gps location known");
            this.Dispatcher.Dispatch(() =>
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(currentLatitude, currentLongitude), Distance.FromMiles(0.5)));
            });
        }
        else
        {
            // Permission not granted?            
            Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] LoadLocationAsync: gps location unknown");
        }
    }

    [Obsolete]
    private void AddPinToAccidentLocation()
    {
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] AddPinToAccidentLocation: eerst removecurrentpin()");
        Device.BeginInvokeOnMainThread(() =>
        {
            RemoveCurrentPin();

            //_viewModel.GetAcciddentAddressLocation();
            /*
            map.Pins.Add(new Pin
            {
                Location = new Location((double)_viewModel.ClaimForm.Latitude, (double)_viewModel.ClaimForm.Longitude),
                Label = $"{_viewModel.ClaimForm.Street} {_viewModel.ClaimForm.Suite} {_viewModel.ClaimForm.ZipCode} {_viewModel.ClaimForm.City}",
                Type = PinType.Generic
            });*/
            _currentPin = new Pin
            {
                Location = new Location((double)_viewModel.ClaimForm.Latitude, (double)_viewModel.ClaimForm.Longitude),
                Label = $"{_viewModel.ClaimForm.Street} {_viewModel.ClaimForm.Suite} {_viewModel.ClaimForm.ZipCode} {_viewModel.ClaimForm.City}",
                Type = PinType.Generic
            };
            Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] AddPinToAccidentLocation: Voeg nieuwe pin toe: " + _currentPin.Address);
            map.Pins.Add(_currentPin);
        });

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        LoadLocationAsync();
        AddPinToAccidentLocation();
    }

    private void RemoveCurrentPin()
    {
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] RemoveCurrentPin: Start");
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] RemoveCurrentPin: _currentPin = " + _currentPin);
        if (_currentPin != null)
        {
            Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] RemoveCurrentPin: _currentPin = " + _currentPin.Address);
            map.Pins.Remove(_currentPin);
            _currentPin = null;
            Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] RemoveCurrentPin: _currentPin leeggemaakt");
        }
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] RemoveCurrentPin: Einde" );
    }

    async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] OnMapClicked: start");
        // Remove the current pin since a new one will be added
        RemoveCurrentPin();
        Debug.WriteLine(DateTime.Now + "[.........][ClaimFormStep2Page] OnMapClicked: RemoveCurrentPin() uitgevoerd");

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