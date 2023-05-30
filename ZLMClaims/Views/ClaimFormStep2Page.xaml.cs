using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
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
        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(51.90083141102497, 4.485235210929851), Distance.FromMiles(5)));
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");
        double latitude = e.Location.Latitude;
        double longitude = e.Location.Longitude;
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [MapClickedEventArgs] Latitude: " + latitude + " and longitude: " + longitude);

    }

}