using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Globalization;
using ZLMClaims.Models;

namespace ZLMClaims.Views;

public partial class ClaimFormStep2Page : ContentPage
{
	public ClaimFormStep2Page()
	{
		InitializeComponent();
	}

    private void OnPrevBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[ClaimFormStep2Page] [OnPrevBtnClicked] [==============] sender => " + sender + " with args => " + e);
        Navigation.PopAsync();
    }

    private void OnNextBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[ClaimFormStep3Page] [OnNextBtnClicked] [==============] sender => " + sender + " with args => " + e);
        //Navigation.PushAsync(new ClaimFormStep3Page());
        var claim = ((VisualElement)sender).BindingContext as Claim;
        Shell.Current.GoToAsync(nameof(ClaimFormStep3Page), true, new Dictionary<string, object>
        {
            {nameof(Claim), claim}
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Set the initial map position (Cirkel 63)
        var initialPosition = new Location(51.461678214915544, 3.5559675108738857);
        map.MoveToRegion(MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMiles(1)));

        // Add a circle overlay with a 500-meter radius around the initial position
        var circle = new Circle
        {
            Center = initialPosition,
            Radius = new Distance(500),
            StrokeWidth = 2,
            StrokeColor = Color.FromArgb("#88FF0000"),
            FillColor = Color.FromArgb("#88FFC0CB")
        };
        map.MapElements.Add(circle);

        // Add a pin marker at a specific location (Hercules Segherslaan 7b)
        var newPin = new Pin
        {
            Location = new Location(51.45956502251781, 3.570303055656289),
            Label = "New Location",
            Address = "123 Main St, San Francisco, CA",
            Type = PinType.Place
        };
        map.Pins.Add(newPin);
    }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");

    }

}