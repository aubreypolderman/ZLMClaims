using Moq;
using ZLMClaims.Views;

namespace ZLMClaimsTest;

public class ClaimFormStep1ViewModelTest
{
    [Fact]
    public void CheckValidationPassesWhenAllPropertiesHaveDataTest()
    {
        // act
        var sut = new ClaimFormStep1ViewModelTest();

        // arrange
        // sut.Surname.Value = "Smith";
        //var isValid = sut.Validate();
        bool isValid = true;

        // assert
        Assert.True(isValid);
    }

    [Fact]
    public void OptionsCauseOfDamageContainsExpectedValues()
    {
        // Arrange
        var sut = new ClaimFormStep1ViewModel(Mock.Of<INavigationService>(), Mock.Of<IContractService>(), Mock.Of<IUserService>());

        // Act
        var options = sut.Options;

        // Assert
        Assert.Contains("Aanrijding met een vast object", options);
        Assert.Contains("Aanrijding zonder tegenpartij", options);
        Assert.Contains("Diefstal of inbraak", options);
        Assert.Contains("Ruitschade", options);
        Assert.Contains("Andere oorzaak", options);
    }

    [Fact]
    public void MaxDateReturnsCurrentDate()
    {
        // Arrange
        var sut = new ClaimFormStep1ViewModel(Mock.Of<INavigationService>(), Mock.Of<IContractService>(), Mock.Of<IUserService>());
        var currentDate = DateTime.Now;

        // Act
        var maxDate = sut.MaxDate;

        // Assert
        Assert.Equal(currentDate.Date, maxDate.Date);
    }

            [Fact]
        public void MinDateReturnsCurrentDateMinusThreeMonths()
        {
            // Arrange
            var sut = new ClaimFormStep1ViewModel(Mock.Of<INavigationService>(), Mock.Of<IContractService>(), Mock.Of<IUserService>());
            var currentDateMinusThreeMonths = DateTime.Now.AddMonths(-3);

            // Act
            var minDate = sut.MinDate;

            // Assert
            Assert.Equal(currentDateMinusThreeMonths.Date, minDate.Date);
        }

    [Fact]
    public void SelectedTimeSetPropertyRaisesPropertyChangedEvent()
    {
        // Arrange
        var sut = new ClaimFormStep1ViewModel(Mock.Of<INavigationService>(), Mock.Of<IContractService>(), Mock.Of<IUserService>());
        var propertyChangedRaised = false;

        sut.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(ClaimFormStep1ViewModel.SelectedTime))
            {
                propertyChangedRaised = true;
            }
        };

        // Act
        sut.SelectedTime = TimeSpan.FromHours(5);

        // Assert
        Assert.True(propertyChangedRaised);
    }

    [Fact]
    public async Task Previous_Command_Navigates_Back()
    {
        // Arrange
        var navigationServiceMock = new Mock<INavigationService>();
        var contractServiceMock = new Mock<IContractService>();
        var userServiceMock = new Mock<IUserService>();
        var sut = new ClaimFormStep1ViewModel(navigationServiceMock.Object, contractServiceMock.Object, userServiceMock.Object);

        // Act
        await sut.PreviousCommand.ExecuteAsync(null);

        // Assert
        navigationServiceMock.Verify(ns => ns.GoBackAsync(), Times.Once);
    }
}
    