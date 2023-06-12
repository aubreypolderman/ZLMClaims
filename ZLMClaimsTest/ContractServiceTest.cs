using Moq;
using Moq.Protected;
using System.Net;
using Xunit;
namespace ZLMClaimsTest;
public class ContractServiceTest
{
   /* Error: Only Android and Ios supported
    [Fact]
    public async Task GetAllContractsByPersonIdAsync_Should_Return_Contracts()
    {
        // Arrange
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(httpMessageHandlerMock.Object);

        var responseContent = "[{ \"Id\": 1, \"Name\": \"Contract 1\" }, { \"Id\": 2, \"Name\": \"Contract 2\" }]";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent)
        };

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var contractService = new ContractService(httpClient);

        // Act
        var contracts = await contractService.GetAllContractsByPersonIdAsync(123);

        // Assert
        Assert.Collection(contracts,
            contract => Assert.Equal(1, contract.Id),
            contract => Assert.Equal(2, contract.Id));
    }
   */
}