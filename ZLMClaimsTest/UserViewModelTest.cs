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
        public void OnEmailConfirmationSwitchToggled()
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
    }
}