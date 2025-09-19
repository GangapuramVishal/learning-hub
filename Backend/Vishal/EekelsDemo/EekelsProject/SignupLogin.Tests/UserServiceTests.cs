using Application.Interfaces;
using Application.Service;
using Domain.SignupLoginEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq.Protected;

namespace WebAPI.Tests
{
    public class UserServiceTests
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private Mock<ILogger<UserService>> _loggerMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_configurationMock.Object, _httpClientFactoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Signup_ShouldReturnSuccessResponse_WhenSignupIsSuccessful()
        {
            // Arrange
            var request = new UserSignupRequest { Email = "test@example.com", Password = "password" };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success")
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            _configurationMock.SetupGet(x => x["Auth0:Domain"]).Returns("test-domain");
            _configurationMock.SetupGet(x => x["Auth0:ClientId"]).Returns("test-client-id");

            // Act
            var result = await _userService.Signup(request);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Success", result.Message);
        }

        //[Test]
        //public async Task Login_ShouldReturnSuccessResponse_WhenLoginIsSuccessful()
        //{
        //    // Arrange
        //    var request = new UserLoginRequest { Email = "test@example.com", Password = "password" };
        //    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent(JsonSerializer.Serialize(new { access_token = "token" }), Encoding.UTF8, "application/json")
        //    };

        //    var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        //    httpMessageHandlerMock.Protected()
        //        .Setup<Task<HttpResponseMessage>>("SendAsync",
        //            ItExpr.IsAny<HttpRequestMessage>(),
        //            ItExpr.IsAny<CancellationToken>())
        //        .ReturnsAsync(responseMessage);

        //    var client = new HttpClient(httpMessageHandlerMock.Object);
        //    _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

        //    _configurationMock.SetupGet(x => x["Auth0:Domain"]).Returns("test-domain");
        //    _configurationMock.SetupGet(x => x["Auth0:ClientId"]).Returns("test-client-id");
        //    _configurationMock.SetupGet(x => x["Auth0:ClientSecret"]).Returns("test-client-secret");
        //    _configurationMock.SetupGet(x => x["Auth0:Audience"]).Returns("test-audience");

        //    // Act
        //    var result = await _userService.Login(request);

        //    // Assert
        //    Assert.AreEqual("Login successful", result.Message);
        //    Assert.AreEqual("token", result.AccessToken);
        //}


        [Test]
        public async Task Signup_ShouldReturnErrorResponse_WhenSignupFails()
        {
            // Arrange
            var request = new UserSignupRequest { Email = "test@example.com", Password = "password" };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error")
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            _configurationMock.SetupGet(x => x["Auth0:Domain"]).Returns("test-domain");
            _configurationMock.SetupGet(x => x["Auth0:ClientId"]).Returns("test-client-id");

            // Act
            var result = await _userService.Signup(request);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Error", result.Message);
        }

        [Test]
        public async Task Login_ShouldReturnErrorResponse_WhenLoginFails()
        {
            // Arrange
            var request = new UserLoginRequest { Email = "test@example.com", Password = "password" };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error")
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            _configurationMock.SetupGet(x => x["Auth0:Domain"]).Returns("test-domain");
            _configurationMock.SetupGet(x => x["Auth0:ClientId"]).Returns("test-client-id");
            _configurationMock.SetupGet(x => x["Auth0:ClientSecret"]).Returns("test-client-secret");
            _configurationMock.SetupGet(x => x["Auth0:Audience"]).Returns("test-audience");

            // Act
            var result = await _userService.Login(request);

            // Assert
            Assert.AreEqual("Login failed", result.Message);
            Assert.IsEmpty(result.AccessToken);
        }
    }
}
