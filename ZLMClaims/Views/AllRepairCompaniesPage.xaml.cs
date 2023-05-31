using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class AllRepairCompaniesPage : ContentPage
{

    private readonly AllRepairCompaniesViewModel _viewModel;

    public AllRepairCompaniesPage(AllRepairCompaniesViewModel vm)
	{
        _viewModel = vm;
        BindingContext = vm;
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();       
        await _viewModel.GetAllRepairCompanies();
    }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");

    }
}