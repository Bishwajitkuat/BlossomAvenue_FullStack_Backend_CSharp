using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.AuthenticationService;
using BlossomAvenue.Service.Repositories.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Service.Authentication
{
    public class AuthManagementTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IJwtManagement> _jwtService;

        public AuthManagementTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _jwtService = new Mock<IJwtManagement>();
        }

        [Fact]
        public async Task Authenticate_WhenUserDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).ReturnsAsync((User)null);
            var authManagement = new AuthManagement(_userRepository.Object, _jwtService.Object);

            // Act
            var result = await authManagement.Authenticate("username", "password");

            // Assert
            Assert.False(result.IsAuthenticated);
        }

        [Fact]
        public async Task Authenticate_WhenUserExists_ReturnsTrue()
        {
            // Arrange
            _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).ReturnsAsync(new User());
            _jwtService.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("token");
            var authManagement = new AuthManagement(_userRepository.Object, _jwtService.Object);

            // Act
            var result = await authManagement.Authenticate("username", "password");

            // Assert
            Assert.True(result.IsAuthenticated);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public void Logout_InvalidatesToken()
        {
            // Arrange
            var authManagement = new AuthManagement(_userRepository.Object, _jwtService.Object);

            // Act
            authManagement.Logout("token");

            // Assert
            _jwtService.Verify(x => x.InvalidateToken("token"), Times.Once);
        }


    }
}
