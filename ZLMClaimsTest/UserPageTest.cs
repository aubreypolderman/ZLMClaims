using ZLMClaims.Views;

namespace ZLMClaimsTest;

public class UserPageTest
{
    [Fact]
    public void EntryTextTest()
    {
        // Arrange
        var userService = Substitute.For<IUserService>();
        var navigationService = Substitute.For<INavigationService>();
        var viewModel = new UserViewModel(navigationService, userService);
        var page = new UserPage(viewModel);
        var entry = page.FindByName<Entry>("MyEntry");
        var expectedText = "test text";

        // Act
        // maybe test the toggle switches?
        entry.Text = expectedText;

        // Assert
        Assert.Equal(expectedText, entry.Text);
    }
}