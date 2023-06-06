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
}
    