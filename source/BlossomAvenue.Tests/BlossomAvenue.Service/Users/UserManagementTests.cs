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
using Microsoft.Extensions.Configuration;

namespace BlossomAvenue.Tests.BlossomAvenue.Service.Users
{

    public class UserManagementTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        private readonly UserManagement _userManagement;

        public UserManagementTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserRoleRepository = new Mock<IUserRoleRepository>();
            _userManagement = new UserManagement(_mockUserRepository.Object, _mockUserRoleRepository.Object, _mockMapper.Object, _mockConfiguration.Object);
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
        public void UserManagement_ShouldHaveActiveInactiveUserMethod() 
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
             await Assert.ThrowsAsync<RecordNotFoundException>(() => _userManagement.ActiveInactiveUser(userId, status));
            
        }
        [Fact]
        public async Task UserManagement_ActiveInacitve_ShouldCallUpdateUserOnce()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var status = true;

            var user = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "a.b@c.com",
                UserRoleId = Guid.NewGuid()
            };

            _mockUserRepository.Setup(x => x.GetUser(userId)).ReturnsAsync(user);

            //Act
            await _userManagement.ActiveInactiveUser(userId, status);

            //Assert
            _mockUserRepository.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
            
        }
        [Fact]
        public void UserManagement_ShouldHaveCreateUserMethod()
        {
            //Arrange
            var type = typeof(UserManagement);

            // Act
            var createUserMethod = type.GetMethod("CreateUser");

            Assert.NotNull(createUserMethod);
        }
        [Fact]
        public async Task UserManagement_CreateUser_ShouldThrowExceptionOnEmailExist() 
        {
            //Arrange
            var newUserDto = new CreateUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "a.b@c.com"
            };

            _mockUserRepository.Setup(x => x.CheckUserExistsByEmail(newUserDto.Email)).ReturnsAsync(true);

            //Act and Assert
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() => _userManagement.CreateUser(newUserDto));
        }
        [Fact]
        public async Task UserManagement_CreateUser_ShouldCallCreateUserOnceAndValidReturn()
        {
            //Arrange
            var newUserDto = new CreateUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "a.b@c.com"
            };

            var newUserEntity = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "a.b@c.com"
            };

            var savedUserDto = new UserDto
            {
                UserId = newUserEntity.UserId,
                FirstName = newUserEntity.FirstName,
                LastName = newUserEntity.LastName,
                Email = newUserEntity.Email
            };

            _mockUserRepository.Setup(x => x.CheckUserExistsByEmail(newUserDto.Email)).ReturnsAsync(false);
            _mockConfiguration.Setup(c => c.GetSection("UserRoles").GetSection("Admin").Value)
            .Returns("Admin");
            _mockUserRoleRepository.Setup(x => x.GetUserRoleByName(It.IsAny<string>())).ReturnsAsync(new UserRole { UserRoleName = "Admin" });
            _mockMapper.Setup(x => x.Map<User>(newUserDto)).Returns(newUserEntity);
            _mockUserRepository.Setup(x => x.CreateUser(newUserEntity)).ReturnsAsync(newUserEntity);
            _mockMapper.Setup(x => x.Map<UserDto>(newUserEntity)).Returns(savedUserDto);

            //Act
            var result = await _userManagement.CreateUser(newUserDto);

            //Assert
            _mockUserRepository.Verify(x => x.CreateUser(newUserEntity), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<UserDto>(result);
            Assert.Equal(Guid.Empty, newUserEntity.UserId);
            Assert.Equal("Admin", newUserEntity.UserRole.UserRoleName);
            Assert.True(newUserEntity.IsUserActive);
        }
    }
}
