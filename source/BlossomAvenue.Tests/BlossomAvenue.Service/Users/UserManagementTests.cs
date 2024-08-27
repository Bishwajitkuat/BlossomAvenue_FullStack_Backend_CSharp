using AutoMapper;
using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.UsersService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Service.Users
{

    public class UserManagementTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserManagement _userManagement;

        public UserManagementTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _userManagement = new UserManagement(_mockUserRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public void UserManagement_ShouldExists()
        {
            //Act 
            var classType = Type.GetType("BlossomAvenue.Service.UsersService.UserManagement, BlossomAvenue.Service");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void UserManagement_ShouldHaveGetUsersMethod()
        {
            //Arrange
            var type = typeof(UserManagement);

            // Act
            var getUsersMethod = type.GetMethod("GetUsers");

            Assert.NotNull(getUsersMethod);
        }
        [Fact]
        public async Task GetUsers_ShouldReturnUserDtoList() 
        {
            //Arrange

            var usersDtos = new List<UserDto>
                {
                    new() { UserId = Guid.NewGuid(), FirstName = "John", LastName="Doe", Email="a.b@c.com", UserRoleId= Guid.NewGuid()},
                    new() { UserId = Guid.NewGuid(), FirstName = "Jane", LastName="Smith", Email="c.d@e.com", UserRoleId= Guid.NewGuid()}
                };

            var users = new List<User>
                {
                    new() { UserId = Guid.NewGuid(), FirstName = "John", LastName="Doe", Email="a.b@c.com", UserRoleId= Guid.NewGuid()},
                    new() { UserId = Guid.NewGuid(), FirstName = "Jane", LastName="Smith", Email="c.d@e.com", UserRoleId= Guid.NewGuid()}
                };

            _mockUserRepository.Setup(x => x.GetUsers(1, 10, null, "lastName", "ASC", null)).ReturnsAsync(users);
            _mockMapper.Setup(x => x.Map<List<UserDto>>(users)).Returns(usersDtos);

            //Act
            var result = await _userManagement.GetUsers(1, 10, null, "lastName", "ASC", null);
            

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserDto>>(result);
            Assert.Equal(users.Count, result.Count);

        }
        [Fact]
        public void UserManagement_ShouldHaveGetUserMethod()
        {
            //Arrange
            var type = typeof(UserManagement);

            // Act
            var getUserMethod = type.GetMethod("GetUser");

            Assert.NotNull(getUserMethod);
        }
        [Fact]
        public async Task UserManagement_GetUser_ShouldReturnUserDto()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var usersDto = new UserDetailedDto
                {
                    UserId = Guid.NewGuid(), 
                    FirstName = "John", 
                    LastName="Doe", 
                    Email="a.b@c.com", 
                    UserRoleId= Guid.NewGuid()
                };

            var user = new User
                { 
                    UserId = Guid.NewGuid(), 
                    FirstName = "John", 
                    LastName="Doe", 
                    Email="a.b@c.com", 
                    UserRoleId= Guid.NewGuid()   
                };

            _mockUserRepository.Setup(x => x.GetUser(guid)).ReturnsAsync(user);
            _mockMapper.Setup(x => x.Map<UserDetailedDto>(user)).Returns(usersDto);

            //Act
            var result = await _userManagement.GetUser(guid);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserDetailedDto>(result);
            Assert.Equal(usersDto.UserId, result.UserId);
            Assert.Equal(usersDto.FirstName, result.FirstName);
            Assert.Equal(usersDto.LastName, result.LastName);
            Assert.Equal(usersDto.Email, result.Email);
            Assert.Equal(usersDto.UserRoleId, result.UserRoleId);

        }

        [Fact]
        public async Task UserManagement_GetUser_ShouldThrowRecrodNotFoundExceptionOnNoRecrods() 
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(x => x.GetUser(userId)).ReturnsAsync((User)null);

            // Act and Assert
            await Assert.ThrowsAsync<RecordNotFoundException>(() => _userManagement.GetUser(userId));
        }

        [Fact]
        public async Task UserManagement_ShouldHaveActiveInactiveUserMethod() 
        {
            //Arrange
            var type = typeof(UserManagement);

            // Act
            var activeInactiveUserMethod = type.GetMethod("ActiveInactiveUser");

            Assert.NotNull(activeInactiveUserMethod);
        }

        [Fact]
        public async Task UserManagement_ActiveInacitve_ShouldThrowExceptionOnNoUser() 
        {
            //Arrange
            var userId = Guid.NewGuid();
            var status = true;

            _mockUserRepository.Setup(x => x.GetUser(userId)).ReturnsAsync((User)null);

            //Act and Assert
             Assert.ThrowsAsync<RecordNotFoundException>(() => _userManagement.ActiveInactiveUser(userId, status));

            
        }
    }
}
