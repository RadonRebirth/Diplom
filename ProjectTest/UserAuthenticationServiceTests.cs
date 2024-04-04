using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UserAuthenticationServiceTests
{
    [TestMethod]
    public void AuthenticateUser_ReturnsTrue_ForValidCredentials()
    {
        // Arrange
        var authService = new UserAuthenticationService();
        string validUsername = "admin";
        string validPassword = "password";

        // Act
        bool result = authService.AuthenticateUser(validUsername, validPassword);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void AuthenticateUser_ReturnsFalse_ForInvalidCredentials()
    {
        // Arrange
        var authService = new UserAuthenticationService();
        string invalidUsername = "user";
        string invalidPassword = "123";

        // Act
        bool result = authService.AuthenticateUser(invalidUsername, invalidPassword);

        // Assert
        Assert.IsFalse(result);
    }
}


