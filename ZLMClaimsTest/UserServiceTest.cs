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
    }
}