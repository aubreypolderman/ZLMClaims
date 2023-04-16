using System.Globalization;

namespace ZLMClaims.Views;

public partial class ClaimFormStep2Page : ContentPage
{
	public ClaimFormStep2Page()
	{
		InitializeComponent();
	}

    private async void OnPickImageBtnClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick Image Please",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
            return;

        var stream = await result.OpenReadAsync();

        myImage.Source = ImageSource.FromStream(() => stream);
    }
    private async void OnPickImagesBtnClicked(object sender, EventArgs e)
    {
        // For custom file types            
        //var customFileType =
        //	new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        //	{
        //		 { DevicePlatform.iOS, new[] { "com.adobe.pdf" } }, // or general UTType values
        //       { DevicePlatform.Android, new[] { "application/pdf" } },
        //		 { DevicePlatform.WinUI, new[] { ".pdf" } },
        //		 { DevicePlatform.Tizen, new[] { "*/*" } },
        //		 { DevicePlatform.macOS, new[] { "pdf"} }, // or general UTType values
        //	});

        var results = await FilePicker.PickMultipleAsync(new PickOptions
        {
            //FileTypes = customFileType
        });

        foreach (var result in results)
        {
            await DisplayAlert("You picked...", result.FileName, "OK");
        }
    }

    public async void OnTakePhotoBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [ClaimFormStep2Page] [OnTakePhotoBtnClicked] sender => " + sender + " with args => " + e);
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            Console.WriteLine("[..............] [ClaimFormStep2Page] [OnTakePhotoBtnClicked] photo => " + photo );

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                Console.WriteLine("[..............] [ClaimFormStep2Page] [OnTakePhotoBtnClicked] localFilePath => " + localFilePath);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);
                Console.WriteLine("[..............] [ClaimFormStep2Page] [OnTakePhotoBtnClicked] sourceStream => " + sourceStream);
                Console.WriteLine("[..............] [ClaimFormStep2Page] [OnTakePhotoBtnClicked] localFileStream => " + localFileStream);

                await sourceStream.CopyToAsync(localFileStream);
                myImage.Source = ImageSource.FromStream(() => sourceStream);
                Console.WriteLine("[..............] [ClaimFormStep2Page] [OnTakePhotoBtnClicked] myImage.Source => " + myImage.Source);
            }
        }
    }

    private void OnPrevBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [ClaimFormStep2Page] [OnPrevBtnClicked]");
        Navigation.PopAsync();
    }
    private void OnNextBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine("[..............] [ClaimFormStep2Page] [OnNextBtnClicked]");
        Navigation.PushAsync(new ClaimFormStep3Page());
    }



}