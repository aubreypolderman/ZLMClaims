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
        Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesPage] [AllRepairCompaniesViewModel] Viewmodel injected");

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
        Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesPage] [OnAppearing] Invoke _viewModel.GetAllRepairCompanies()");        
        await _viewModel.GetAllRepairCompanies();
    }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [AllRepairCompaniesPage] [OnMapClicked] " + e.Location.Latitude + "," +  e.Location.Longitude);
        System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");

    }
}