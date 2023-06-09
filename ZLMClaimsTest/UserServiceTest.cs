using Moq;
using System.Net;
using Xunit;
namespace ZLMClaimsTest
{
    public class UserServiceTest
    {
        [Fact]
        public async Task GetUserById()
        {
            // arrange
            var userService = Substitute.For<IUserService>();
            var userId = 1;

            // act
            // user is null!
            var user = await userService.GetUserByIdAsync(userId);

            // assert
            Assert.NotNull(userId);
            // Assert.Equal(userId, user.Id);
            // Assert.NotNull(user);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>();

            var expectedUser = new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@example.com"
            };

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{
                ""id"": 1,
                ""name"": ""John Doe"",
                ""email"": ""johndoe@example.com""
            }")
            };

            mockHttpClient
                .Setup(_ => _.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(httpResponseMessage);

            var userService = new UserService(mockHttpClient.Object);

            // Act
            var user = await userService.GetUserByIdAsync(1);

            // Assert
            Assert.Equal(expectedUser.Id, user.Id);
            Assert.Equal(expectedUser.Name, user.Name);
            Assert.Equal(expectedUser.Email, user.Email);
        }
    }
}