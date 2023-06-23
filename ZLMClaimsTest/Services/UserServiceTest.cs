using Moq;
using System.Net;
using Xunit;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaimsTest.Services;

public class UserServiceTest
{

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        int userId = 1;
        var expectedUser = new User
        {
            Id = userId,
            Name = "Aubrey Polderman",
            Email = "Aubrey Polderman"
        };

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(@"{
            ""id"": 1,
            ""name"": ""Aubrey Polderman"",
            ""email"": ""Aubrey Polderman""
        }")
        };

        mockHttpClient
            .Setup(_ => _.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponseMessage);

        var userService = new UserService(mockHttpClient.Object);

        // Act
        var user = await userService.GetUserByIdAsync(userId);

        // Assert
        Assert.Equal(expectedUser.Id, user.Id);
        Assert.Equal(expectedUser.Name, user.Name);
        Assert.Equal(expectedUser.Email, user.Email);
    }
}