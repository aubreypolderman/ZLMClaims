using System.Globalization;

namespace ZLMClaims.Views;

public partial class ClaimFormStep1Page : ContentPage
{
    private DateTime _minimumDate;
    private DateTime _maximumDate;

    public DateTime MinimumDate
    {
        get { return _minimumDate; }
        set
        {
            if (_minimumDate != value)
            {
                _minimumDate = value;
                OnPropertyChanged(nameof(MinimumDate));
            }
        }
    }

    public DateTime MaximumDate
    {
        get { return _maximumDate; }
        set
        {
            if (_maximumDate != value)
            {
                _maximumDate = value;
                OnPropertyChanged(nameof(MaximumDate));
            }
        }
    }

    public ClaimFormStep1Page()
	{
		InitializeComponent();

        MinimumDate = DateTime.Now.AddDays(-365);
        MaximumDate = DateTime.Now;

        Console.WriteLine("[..............] [ClaimFormStep1Page] [Constructor] MinimumDate => " + MinimumDate);
        Console.WriteLine("[..............] [ClaimFormStep1Page] [Constructor] MaximumDate => " + MaximumDate);
    }

    private void OnPrevBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnPrevBtnClicked] sender => " + sender + " with args => " + e);
        Navigation.PopAsync();
    }

    private void OnNextBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnNextBtnClicked] sender => " + sender + " with args => " + e);
        Navigation.PushAsync(new ClaimFormStep2Page());
    }


    // datepicker
    void OnDateSelected(object sender, DateChangedEventArgs args)
    {
        //Console.WriteLine($"Current culture: {CultureInfo.CurrentCulture.Name}");
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] sender => " + sender + " with args => " + args);
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] Date => " + ((DatePicker)sender).Date);
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] Date.ToString(\"d\") => " + ((DatePicker)sender).Date.ToString("d"));
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] Date.ToString(\"D\") => " + ((DatePicker)sender).Date.ToString("D"));
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] Date.ToString(\"f\") => " + ((DatePicker)sender).Date.ToString("f"));
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] Date.ToString(\"F\") => " + ((DatePicker)sender).Date.ToString("F"));

        CultureInfo culture = new CultureInfo("en-SG");
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] Date.ToString(\"D\") & culture => " + ((DatePicker)sender).Date.ToString("d", culture));
        culture = CultureInfo.InvariantCulture;
        Console.WriteLine("[..............] [ClaimFormStep1Page] [OnDateSelected] Date.ToString(\"d\") & culture => " + ((DatePicker)sender).Date.ToString("d", culture));
    }

}