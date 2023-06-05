namespace ZLMClaimsTest
{
    public class ClaimFormStep2ViewModelTest
    {
        [Fact]
        public async void GetCurrentLocationTest()
        {
            // arrange
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            var viewModel = new ClaimFormStep2ViewModel(navigationService, dialogService);
            double expectedLatitude = 51.8985;
            double expectedLongitude = 4.5116;

            // act
            (double receivedLatitude, double receivedLongitude) = await viewModel.GetCurrentLocation();

            // assert
            receivedLatitude.Equals(expectedLatitude);
            receivedLongitude.Equals(expectedLongitude);
        }

        [Fact]
        public void CalculateDistanceInKmTest()
        {
            // arrange
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            var viewModel = new ClaimFormStep2ViewModel(navigationService, dialogService);
            double currentLongitude = 4.5116;
            double currentLatitude = 51.8985;
            double selectedLongitude = 4.434807604412547;
            double selectedLatitude = 51.86404076058215;
            string expectedDistanceInKm = "9.4";

            // act
            string receivedDistanceInKm = viewModel.CalculateDistanceInKm(currentLongitude, currentLatitude, selectedLongitude, selectedLatitude);

            // assert
            receivedDistanceInKm.Equals(expectedDistanceInKm);
        }


    }
}