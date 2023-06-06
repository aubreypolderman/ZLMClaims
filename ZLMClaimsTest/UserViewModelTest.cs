namespace ZLMClaimsTest
{
    public class UserViewModelTest
    {

        [Fact]
        public void OnEmailConfirmationSwitchToggledTest()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var sut = new UserViewModel(navigationService, userService);

            // act
            sut.switchToggleEmailConfirmation = true;

            // assert
            sut.switchToggleEmailConfirmation.Should().BeTrue();
        }

        [Fact]
        public void OnLanguageSwitchToggledTest()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var viewModel = new UserViewModel(navigationService, userService);

            // act
            //viewModel.OnLanguageSwitchToggled();

            // assert
            //viewModel.switchToggleEnglish.Should().BeEquivalentTo("User X Profile");
        }
    }
}