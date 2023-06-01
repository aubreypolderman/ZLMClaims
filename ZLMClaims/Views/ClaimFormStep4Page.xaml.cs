using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class ClaimFormStep4Page : ContentPage
{
	public ClaimFormStep4Page(ClaimFormStep4ViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
    public async void OnTakePhotoBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] START ");
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] Photo => " + photo);

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] Photo.FileName => " + photo.FileName);                
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] localFilePath => " + localFilePath);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] sourceStream => " + sourceStream);
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] localFileStream => " + localFileStream);

                await sourceStream.CopyToAsync(localFileStream);
                
                // Set the image source
                myImage.Source = ImageSource.FromFile(localFilePath);                
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] myImage.Source => " + myImage.Source);

                // Close the source stream
                sourceStream.Close();
                //myImage.Source = ImageSource.FromStream(() => sourceStream);
                // Set the image source
                myImage.Source = ImageSource.FromFile(localFilePath);

                // Update the ImageFileName property in the viewmodel
                ((ClaimFormStep4ViewModel)BindingContext).ImageFileName = photo.FileName;
            }
        }
    }

}