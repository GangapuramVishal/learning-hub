using Application.Interfaces;
using Domain.SignupLoginEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests
{
    public class UsersControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<UsersController>> _loggerMock;
        private UsersController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UsersController>>();
            _controller = new UsersController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Signup_ShouldReturnOk_WhenSignupIsSuccessful()
        {
            // Arrange
            var request = new UserSignupRequest { Email = "test@example.com", Password = "password" };
            var response = new SignupResponse { IsSuccess = true, Message = "Success" };
            _userServiceMock.Setup(x => x.Signup(It.IsAny<UserSignupRequest>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Signup(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Success", result.Value);
        }

        [Test]
        public async Task Signup_ShouldReturnBadRequest_WhenSignupFails()
        {
            // Arrange
            var request = new UserSignupRequest { Email = "test@example.com", Password = "password" };
            var response = new SignupResponse { IsSuccess = false, Message = "Error" };
            _userServiceMock.Setup(x => x.Signup(It.IsAny<UserSignupRequest>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Signup(request) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Error", result.Value);
        }

        [Test]
        public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var request = new UserLoginRequest { Email = "test@example.com", Password = "password" };
            var response = new LoginResponse { Message = "Login successful", AccessToken = "token" };
            _userServiceMock.Setup(x => x.Login(It.IsAny<UserLoginRequest>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Login(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(response, result.Value);
        }

        [Test]
        public async Task Login_ShouldReturnBadRequest_WhenLoginFails()
        {
            // Arrange
            var request = new UserLoginRequest { Email = "test@example.com", Password = "password" };
            var response = new LoginResponse { Message = "Login failed", AccessToken = string.Empty };
            _userServiceMock.Setup(x => x.Login(It.IsAny<UserLoginRequest>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Login(request) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Login failed", result.Value);
        }
    }
}
