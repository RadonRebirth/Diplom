using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestASPProject.Tests
{
    [TestClass]
    public class RegistrationServiceTests
    {
        [TestMethod]
        public void RegisterUser_ReturnsTrue_WhenNewUser()
        {
            // Arrange
            var registrationService = new RegistrationService();
            string newUser = "newUser";

            // Act
            bool result = registrationService.RegisterUser(newUser);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsFalse_WhenUserAlreadyExists()
        {
            // Arrange
            var registrationService = new RegistrationService();
            string existingUser = "existingUser";
            registrationService.RegisterUser(existingUser); // Регистрация пользователя

            // Act
            bool result = registrationService.RegisterUser(existingUser); // Попытка повторной регистрации

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ReturnsFalse_WhenUsernameIsEmpty()
        {
            // Arrange
            var registrationService = new RegistrationService();
            string emptyUser = "";

            // Act
            bool result = registrationService.RegisterUser(emptyUser);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
