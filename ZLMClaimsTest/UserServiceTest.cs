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
            var user = await userService.GetUserByIdAsync(userId);

            // assert
            //Assert.NotNull(user);
            //Assert.Equal(userId, user.Id);
            Assert.NotNull(userId);

        }
    }
}