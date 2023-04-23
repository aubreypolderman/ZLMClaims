namespace ZLMClaimsTest
{
    public class UserViewModelTest
    {
        [Fact]
        public void OnInitAppTest()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var viewModel = new UserViewModel(navigationService, userService);

            // act
            viewModel.InitApp();

            // assert
            viewModel.Title.Should().BeEquivalentTo("User X Profile");
        }

        [Fact]
        public void OnEmailConfirmationSwitchToggledTest()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var viewModel = new UserViewModel(navigationService, userService);

            // act
            viewModel.switchToggleEmailConfirmation = true;

            // assert
            viewModel.switchToggleEmailConfirmation.Should().BeTrue();
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