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
        _viewModel = vm;
        BindingContext = _viewModel;
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

    private string ConvertBase64ToImage(String base64EncodedImage)
    {
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
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

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
                var base64EncodedImage = Convert.ToBase64String(memoryStream.ToArray());

                // Update the ImageFile property in the viewmodel
                _viewModel.ImageFileName = photo.FileName;
                _viewModel.Base64EncodedImage = base64EncodedImage;
            }
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Convert base64-encoded string to image        
        _viewModel.SetBase64EncodedImage(_viewModel.ClaimForm.Image2);
        ConvertBase64ToImage(_viewModel.Base64EncodedImage);
    }
}