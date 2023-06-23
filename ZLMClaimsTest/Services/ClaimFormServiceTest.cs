using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaimsTest.Services;

public class ClaimFormServiceTest
{
    [Fact]
    public async Task GetClaimFormAsync_ReturnsClaimForm()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var expectedClaimForm = new ClaimForm
        {
            Id = 1,
            ContractId = 1,
            DateOfOccurence = DateTime.Now,
            QCauseOfDamage = "Accident"
        };

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(expectedClaimForm), Encoding.UTF8, "application/json")
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var claimFormService = new ClaimFormService(httpClient);

        // Act
        var claimForm = await claimFormService.GetClaimFormAsync(expectedClaimForm.Id);

        // Assert
        Assert.Equal(expectedClaimForm.Id, claimForm.Id);
        Assert.Equal(expectedClaimForm.ContractId, claimForm.ContractId);
        Assert.Equal(expectedClaimForm.DateOfOccurence, claimForm.DateOfOccurence);
        Assert.Equal(expectedClaimForm.QCauseOfDamage, claimForm.QCauseOfDamage);
    }

    [Fact]
    public async Task GetAllClaimFormsByPersonIdAsync_ReturnsClaimForms()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var expectedClaimForms = new List<ClaimForm>
            {
                new ClaimForm { Id = 1, ContractId = 1, DateOfOccurence = DateTime.Now, QCauseOfDamage = "Accident" },
                new ClaimForm { Id = 2, ContractId = 2, DateOfOccurence = DateTime.Now.AddDays(-1), QCauseOfDamage = "Theft" }
            };

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(expectedClaimForms), Encoding.UTF8, "application/json")
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var claimFormService = new ClaimFormService(httpClient);

        // Act
        var claimForms = await claimFormService.GetAllClaimFormsByPersonIdAsync(1);

        // Assert
        Assert.Equal(expectedClaimForms.Count, claimForms.Count());

        int i = 0;
        foreach (var expectedClaimForm in expectedClaimForms)
        {
            var actualClaimForm = claimForms.ElementAt(i);
            Assert.Equal(expectedClaimForm.Id, actualClaimForm.Id);
            Assert.Equal(expectedClaimForm.ContractId, actualClaimForm.ContractId);
            Assert.Equal(expectedClaimForm.DateOfOccurence, actualClaimForm.DateOfOccurence);
            Assert.Equal(expectedClaimForm.QCauseOfDamage, actualClaimForm.QCauseOfDamage);

            i++;
        }
    }

    [Fact]
    public async Task SaveClaimFormAsync_UpdatesClaimForm_WhenClaimExists()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var existingClaimForm = new ClaimForm { Id = 1, ContractId = 1, DateOfOccurence = DateTime.Now, QCauseOfDamage = "Accident" };
        var updatedClaimForm = new ClaimForm { Id = 1, ContractId = 1, DateOfOccurence = DateTime.Now.AddDays(-1), QCauseOfDamage = "Theft" };

        var existingClaimResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(existingClaimForm), Encoding.UTF8, "application/json")
        };

        var updateClaimResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(existingClaimResponse);

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(updateClaimResponse);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var claimFormService = new ClaimFormService(httpClient);

        // Act
        var result = await claimFormService.SaveClaimFormAsync(updatedClaimForm);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task SaveClaimFormAsync_CreatesClaimForm_WhenClaimDoesNotExist()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var newClaimForm = new ClaimForm { Id = 1, ContractId = 1, DateOfOccurence = DateTime.Now, QCauseOfDamage = "Accident" };

        var existingClaimResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };

        var createClaimResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(existingClaimResponse);

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(createClaimResponse);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var claimFormService = new ClaimFormService(httpClient);

        // Act
        var result = await claimFormService.SaveClaimFormAsync(newClaimForm);

        // Assert
        Assert.True(result);
    }
}