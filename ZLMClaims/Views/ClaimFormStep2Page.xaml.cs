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
        string geocodeData = await _viewModel.GetGeocodeReverseData(latitude, longitude);
        Console.WriteLine(geocodeData );

        // Dit gaat fout
        // Claim.Address address = JsonConvert.DeserializeObject<Claim.Address>(geocodeData);
        //Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [address] " + address);

        // Vul de velden in het Address object in
        /*
        _viewModel.Claim.AccidentAddress.Street = address.Street;
        _viewModel.Claim.AccidentAddress.Suite = address.Suite;
        _viewModel.Claim.AccidentAddress.City = address.City;
        _viewModel.Claim.AccidentAddress.Zipcode = address.Zipcode;

        // Optioneel: Als de geocoderingsgegevens ook latitude en longitude bevatten, kun je die ook instellen
        _viewModel.Claim.AccidentAddress.Geo.Latitude = latitude;
        _viewModel.Claim.AccidentAddress.Geo.Longitude = longitude;
        */
    }

}