using Moq;
using MoqForService;

namespace MoqForServiceTest
{
    // NUnit test
    [TestFixture]
    public class UserManagerTests
    {
        [Test]
        public void GetUser_UserExists_ReturnsUser()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(u => u.GetUserById(1)).Returns(new User { Id = 1, Name = "John" });

            var userManager = new UserManager(userServiceMock.Object);

            // Act
            var result = userManager.GetUser(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("John", result.Name);
            
        }

        [Test]
        public void GetUser_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(u => u.GetUserById(It.IsAny<int>())).Returns((User)null);

            var userManager = new UserManager(userServiceMock.Object);

            // Act
            var result = userManager.GetUser(2); // Assuming user with ID 2 does not exist

            // Assert
            Assert.IsNull(result);
        }
    }
}