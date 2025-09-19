using Moq;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.SignupLoginEntities;

namespace WebAPI.Tests
{
    [TestFixture]
    public class SignupLoginTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<UsersController>> _loggerMock;
        private UsersController _controller;

        [SetUp]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UsersController>>();
            _controller = new UsersController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Login_WhenCalled_ReturnsOkResultWithToken()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                Email = "test@example.com",
                Password = "Password123!"
            };
            var loginResponse = new LoginResponse
            {
                Message = "Login successful",
                AccessToken = "fake-jwt-token"
            };
            _userServiceMock.Setup(us => us.Login(request)).ReturnsAsync(loginResponse);

            // Act
            var result = await _controller.Login(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(loginResponse, okResult.Value);
        }

        //[Test]
        //public async Task Login_WhenLoginFails_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var request = new UserLoginRequest
        //    {
        //        Email = "test@example.com",
        //        Password = "wrongpassword"
        //    };
        //    var loginResponse = new LoginResponse
        //    {
        //        Message = "Login failed",
        //        AccessToken = string.Empty
        //    };
        //    _userServiceMock.Setup(us => us.Login(request)).ReturnsAsync(loginResponse);

        //    // Act
        //    var result = await _controller.Login(request);

        //    // Assert
        //    Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        //    var badRequestResult = result as BadRequestObjectResult;
        //    Assert.IsNotNull(badRequestResult);
        //    Assert.AreEqual(loginResponse, badRequestResult.Value);
        //}

        [Test]
        public async Task Signup_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var request = new UserSignupRequest
            {
                Email = "test@example.com",
                Password = "Password123!"
            };
            var signupResponse = new SignupResponse
            {
                IsSuccess = true,
                Message = "Signup successful"
            };
            _userServiceMock.Setup(us => us.Signup(request)).ReturnsAsync(signupResponse);

            // Act
            var result = await _controller.Signup(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(signupResponse.Message, okResult.Value);
        }

        [Test]
        public async Task Signup_WhenSignupFails_ReturnsBadRequest()
        {
            // Arrange
            var request = new UserSignupRequest
            {
                Email = "test@example.com",
                Password = "Password123!"
            };
            var signupResponse = new SignupResponse
            {
                IsSuccess = false,
                Message = "Signup failed"
            };
            _userServiceMock.Setup(us => us.Signup(request)).ReturnsAsync(signupResponse);

            // Act
            var result = await _controller.Signup(request);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(signupResponse.Message, badRequestResult.Value);
        }
    }
}