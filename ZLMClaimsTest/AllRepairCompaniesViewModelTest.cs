using Moq;
using ZLMClaims.Views;

namespace ZLMClaimsTest;

public class AllRepairCompaniesViewModelTest
{
    [Fact]
    public async Task GetAllRepairCompanies_Should_Add_RepairCompanies_To_ObservableCollection()
    {
        // Arrange
        var navigationServiceMock = new Mock<INavigationService>();
        var repairCompanyServiceMock = new Mock<IRepairCompanyService>();
        var dialogServiceMock = new Mock<IDialogService>();

        var sut = new AllRepairCompaniesViewModel(navigationServiceMock.Object, repairCompanyServiceMock.Object, dialogServiceMock.Object);

        var repairCompanies = new[]
        {
                new RepairCompany { Name = "Company 1", Latitude = 1.0, Longitude = 2.0 },
                new RepairCompany { Name = "Company 2", Latitude = 3.0, Longitude = 4.0 }
            };

        repairCompanyServiceMock.Setup(service => service.GetRepairCompaniesAsync()).ReturnsAsync(repairCompanies);

        // Act
        await sut.GetAllRepairCompanies();

        // Assert
        Assert.Equal(2, sut.RepairCompanies.Count);
        Assert.Equal("Company 1", sut.RepairCompanies[0].Name);
        Assert.Equal("Company 2", sut.RepairCompanies[1].Name);
    }

}
    