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
            var sut = new ClaimFormStep2ViewModel(navigationService, dialogService);
            double expectedLatitude = 51.8985;
            double expectedLongitude = 4.5116;

            // act
            (double receivedLatitude, double receivedLongitude) = await sut.GetCurrentLocation();

            // assert
            receivedLatitude.Equals(expectedLatitude);
            receivedLongitude.Equals(expectedLongitude);
        }

        [Fact]
        public void CalculateDistanceCurrentLocationAndOtherLocationInKmTest()
        {
            // arrange
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            var sut = new ClaimFormStep2ViewModel(navigationService, dialogService);
            double currentLongitude = 4.5116;
            double currentLatitude = 51.8985;
            double selectedLongitude = 4.434807604412547;
            double selectedLatitude = 51.86404076058215;
            string expectedDistanceInKm = "6.5";

            // act
            string receivedDistanceInKm = sut.CalculateDistanceInKm(currentLongitude, currentLatitude, selectedLongitude, selectedLatitude);

            // assert
            // receivedDistanceInKm.Equals(expectedDistanceInKm);
            Assert.Equal(expectedDistanceInKm, receivedDistanceInKm);
        }

        [Fact]
        public async void GetGeoLocationOfAddressInRotterdamTest()
        {
            // arrange
            var dialogService = Substitute.For<IDialogService>();
            var navigationService = Substitute.For<INavigationService>();
            var sut = new ClaimFormStep2ViewModel(navigationService, dialogService);
            double currentLongitude = 4.5116;
            double currentLatitude = 51.8985;
            string street = "Beverstraat";
            string houseNumber = "9C";
            string postalCode = "3074SC";
            string city = "Rotterdam";
            string countryName = "Nederland"; 

            // act
            Dictionary<string, string> addressData = await sut.GetGeocodeReverseData(currentLatitude, currentLongitude);
            string placeStreet = addressData["Thoroughfare"];
            string placeHouseNumber = addressData["SubThoroughfare"];
            string placePostalCode = addressData["PostalCode"];
            string placeCity = addressData["Locality"];
            string placeCountryName = addressData["CountryName"];

            // assert
            Assert.Equal(street, placeStreet);
            Assert.Equal(houseNumber, placeHouseNumber);
            Assert.Equal(postalCode, placePostalCode);
            Assert.Equal(city, placeCity);
            Assert.Equal(countryName, placeCountryName);
        }

    }
}