using System.Globalization;
using ZLMClaims.Models;
using ZLMClaims.ViewModels;
using System.IO;

namespace ZLMClaims.Views;

public partial class ClaimFormStep4Page : ContentPage
{
    private readonly ClaimFormStep4ViewModel _viewModel;
    public ClaimFormStep4Page(ClaimFormStep4ViewModel vm)
    {
        BindingContext = vm;
        _viewModel = vm;
        InitializeComponent();
    }

    private async Task<string> ConvertImageToBase64(ImageSource imageSource)
    {
        if (imageSource is StreamImageSource streamImageSource)
        {
            using (Stream stream = await streamImageSource.Stream(CancellationToken.None))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        return null;
    }

    private async Task<string> ConvertBase64ToImage(String base64EncodedImage)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [ConvertBase64ToImage] start");
        if (!string.IsNullOrEmpty(base64EncodedImage))
        {
            byte[] imageBytes = Convert.FromBase64String(base64EncodedImage);
            MemoryStream imageDecodeStream = new MemoryStream(imageBytes);
            myImage.Source = ImageSource.FromStream(() => imageDecodeStream);  
        }

        return null;
    }

    public async void OnTakePhotoBtnClicked(object sender, EventArgs e)
    {
        Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] START ");
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] Photo.FileName => " + photo.FileName);
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] localFilePath => " + localFilePath);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);
                await sourceStream.CopyToAsync(localFileStream);

                // show image on page
                myImage.Source = ImageSource.FromFile(localFilePath);

                // Close the source stream
                sourceStream.Close();

                using var imageEncodeStream = await photo.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                imageEncodeStream.CopyTo(memoryStream);
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] before base64-encode image");
                var base64EncodedImage = Convert.ToBase64String(memoryStream.ToArray());
                Console.WriteLine(DateTime.Now + "[..............] [ClaimFormStep4Page] [OnTakePhotoBtnClicked] base64EncodedImage => " + base64EncodedImage);

                // Update the ImageFile property in the viewmodel
                _viewModel.ImageFileName = photo.FileName;
                _viewModel.Base64EncodedImage = base64EncodedImage;

            }
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Convert base64-encoded string to image        
        _viewModel.SetBase64EncodedImage(_viewModel.ClaimForm.Image2);      
        await ConvertBase64ToImage(_viewModel.Base64EncodedImage);
    }
}