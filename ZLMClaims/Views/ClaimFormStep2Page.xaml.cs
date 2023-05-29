using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep2Page : ContentPage
{
    public ClaimFormStep2Page(ClaimFormStep2ViewModel vm)

    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep2Page] [ClaimFormStep2ViewModel] viewmodel injected");
        BindingContext = vm;
        InitializeComponent();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");
    }

}