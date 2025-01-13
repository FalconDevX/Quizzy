using NUnit.Framework;
using System;
using System.Threading.Tasks;
using WPF;

namespace UnitTest
{
    [TestFixture]
    public class AppTests
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
        }

        [Test]
        public async Task IsLoginTakenApi_ShouldReturnTrue_WhenLoginExists()
        {
            // Arrange
            string existingLogin = "test";

            // Act
            bool result = await _userService.IsLoginTakenApi(existingLogin);

            // Assert
            Assert.IsTrue(result, "Login should be taken.");
        }

        [Test]
        public async Task IsLoginTakenApi_ShouldReturnFalse_WhenLoginDoesNotExist()
        {
            // Arrange
            string nonExistingLogin = "newUser";

            // Act
            bool result = await _userService.IsLoginTakenApi(nonExistingLogin);

            // Assert
            Assert.IsFalse(result, "Login should not be taken.");
        }

        [Test]
        public void HashPassword_ShouldReturnValidHash()
        {
            // Arrange
            string password = "Test1234";

            // Act
            string hashedPassword = UserService.HashPassword(password);

            // Assert
            Assert.IsNotNull(hashedPassword, "Hashed password should not be null.");
            Assert.AreNotEqual(password, hashedPassword, "Hashed password should not equal the original password.");
        }

        [Test]
        public void CurrentUser_ShouldBeNullInitially()
        {
            // Assert
            Assert.IsNull(CurrentUser.Login, "Login should be null initially.");
            Assert.IsNull(CurrentUser.Email, "Email should be null initially.");
            Assert.AreEqual(0, CurrentUser.UserId, "UserId should be 0 initially.");
        }

        [Test]
        public void CurrentUser_ShouldUpdateCorrectly()
        {
            // Arrange
            CurrentUser.Login = "TestUser";
            CurrentUser.Email = "test@example.com";
            CurrentUser.UserId = 1;

            // Assert
            Assert.AreEqual("TestUser", CurrentUser.Login);
            Assert.AreEqual("test@example.com", CurrentUser.Email);
            Assert.AreEqual(1, CurrentUser.UserId);
        }

        [Test]
        public async Task LoginUser_ShouldAuthenticateCorrectly()
        {
            // Arrange
            string login = "test";
            string password = "Test#@!";

            // Act
            bool isAuthenticated = await _userService.LoginUser(login, password);

            // Assert
            Assert.IsTrue(isAuthenticated, "User should be authenticated with valid credentials.");
        }

        [Test]
        public async Task LoginUser_ShouldFailWithInvalidCredentials()
        {
            // Arrange
            string login = "invalidUser";
            string password = "InvalidPassword";

            // Act
            bool isAuthenticated = await _userService.LoginUser(login, password);

            // Assert
            Assert.IsFalse(isAuthenticated, "User should not be authenticated with invalid credentials.");
        }

        [Test]
        public async Task ChangeUserLogin_ShouldUpdateLoginSuccessfully()
        {
            int userId = 11;
            string newLogin = "UpdatedLogin";

            bool result = await _userService.ChangeUserLogin(userId, newLogin);

            Assert.IsTrue(result, "User login should be updated successfully.");
        }

        [Test]
        public async Task ChangeUserPassword_ShouldUpdatePasswordSuccessfully()
        {
            // Arrange
            int userId = 11;
            string newPassword = UserService.HashPassword("NewPassword123");

            // Act
            bool result = await _userService.ChangeUserPassword(userId, newPassword);

            // Assert
            Assert.IsTrue(result, "User password should be updated successfully.");
        }
    }
}
