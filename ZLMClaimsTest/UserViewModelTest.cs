namespace ZLMClaimsTest
{
    public class UserViewModelTest
    {

        [Fact]
        public void OnLanguageSwitchToggledOnShouldBeTrueTest()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var sut = new UserViewModel(navigationService, userService);

            // act
            sut.switchToggleLanguage = true;

            // assert
            sut.switchToggleLanguage.Should().BeTrue();
        }
        [Fact]
        public void OnThemeSwitchDarkThemeToggledOnShouldBeTrueTest()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var sut = new UserViewModel(navigationService, userService);

            // act
            sut.switchToggleDarkTheme = true;

            // assert
            sut.switchToggleDarkTheme.Should().BeTrue();
        }
        [Fact]
        public void OnThemeSwitchDarkThemeToggledOnShouldBeFalseTest()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var sut = new UserViewModel(navigationService, userService);

            // act
            sut.switchToggleDarkTheme = false;

            // assert
            sut.switchToggleDarkTheme.Should().BeFalse();
        }
    }
}