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
        Console.WriteLine("[..............] [ClaimFormStep2Page] [ClaimFormStep2ViewModel] viewmodel injected");
        BindingContext = vm;
        InitializeComponent();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Set the initial map position (Cirkel 63)
        //var initialPosition = new Location(51.46166484597024, 3.555967510908632);
        //map.MoveToRegion(MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMiles(1)));

        // Add a circle overlay with a 5000-meter radius around the initial position
        //var circle = new Circle
        //{
        //    Center = initialPosition,
        //    Radius = new Distance(500),
        //    StrokeWidth = 2,
        //    StrokeColor = Color.FromArgb("#88FF0000"),
        //    FillColor = Color.FromArgb("#88FFC0CB")
       // };
       // map.MapElements.Add(circle);

        // Add a pin marker at a specific location (Hercules Segherslaan 7b, Vlissingen)
       // var newPin = new Pin
       // {
       //     Location = new Location(51.45956502251781, 3.570303055656289),
       //     Label = "Reparateur van den Berg",
       //     Address = "Hercules Segherslaan 7a",
       //     Type = PinType.Place
       // };
        //map.Pins.Add(newPin);

        // Add a pin marker at a specific location (Waalstraat 70, Middelburg)
        /*
        var newPin1 = new Pin
        {
            Location = new Location(51.485769033751126, 3.6025929397454894),
            Label = "Reparateur Bosch autoschade",
            Address = "Waalstraat 70",
            Type = PinType.Place
        };
        map.Pins.Add(newPin1);
        // 
        // Add a pin marker at a specific location (Troelstraweg 128, Vlissingen)
        var newPin2 = new Pin
        {
            Location = new Location(51.46218310671052, 3.5572686109086558),
            Label = "Reparateur van Vliet",
            Address = "Troelstraweg 128",
            Type = PinType.Place
        };
        map.Pins.Add(newPin2);
        */
    }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");

    }

}