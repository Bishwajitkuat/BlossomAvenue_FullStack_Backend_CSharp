using AutoMapper;
using BlossomAvenue.Service.Repositories.Users;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.UsersService;
using Moq;
using Microsoft.Extensions.Configuration;
using BlossomAvenue.Service.Repositories.Cities;
using BlossomAvenue.Service.Cryptography;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Service.Shared_Dtos;
using BlossomAvenue.Service.UsersService.Dtos;

namespace BlossomAvenue.Tests.BlossomAvenue.Service.Users
{

    public class UserManagementTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly UserManagement _userManagement;
        private readonly Mock<IPasswordHasher> _passwordHasher;

        public UserManagementTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserRoleRepository = new Mock<IUserRoleRepository>();
            _mockCityRepository = new Mock<ICityRepository>();
            _passwordHasher = new Mock<IPasswordHasher>();
            _userManagement = new UserManagement(
                _mockUserRepository.Object,
                _mockUserRoleRepository.Object,
                _mockCityRepository.Object,
                _mockMapper.Object,
                _mockConfiguration.Object,
                _passwordHasher.Object
                );
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

            var usersDtos = new List<global::BlossomAvenue.Service.UsersService.Dtos.UserDto>
                {
                    new() { UserId = Guid.NewGuid(), FirstName = "John", LastName="Doe", Email="a.b@c.com", UserRoleId= Guid.NewGuid()},
                    new() { UserId = Guid.NewGuid(), FirstName = "Jane", LastName="Smith", Email="c.d@e.com", UserRoleId= Guid.NewGuid()}
                };

            var users = new List<User>
                {
                    new() { UserId = Guid.NewGuid(), FirstName = "John", LastName="Doe", Email="a.b@c.com", UserRoleId= Guid.NewGuid()},
                    new() { UserId = Guid.NewGuid(), FirstName = "Jane", LastName="Smith", Email="c.d@e.com", UserRoleId= Guid.NewGuid()}
                };

            var userSearch = new UsersQueryDto
            {
                PageNo = 1,
                PageSize = 10,
                Search = null,
                OrderWith = UsersOrderWith.LastName.ToString(),
                OrderBy = OrderBy.ASC,
                UserRoleId = null
            };

            _mockUserRepository.Setup(x => x.GetUsers(userSearch)).ReturnsAsync(users);
            _mockMapper.Setup(x => x.Map<List<global::BlossomAvenue.Service.UsersService.Dtos.UserDto>>(users)).Returns(usersDtos);

            //Act
            var result = await _userManagement.GetUsers(userSearch);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<global::BlossomAvenue.Service.UsersService.Dtos.UserDto>>(result);
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
                LastName = "Doe",
                Email = "a.b@c.com",
                UserRoleId = Guid.NewGuid()
            };

            var user = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "a.b@c.com",
                UserRoleId = Guid.NewGuid()
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
            var newUserDto = new global::BlossomAvenue.Service.UsersService.Dtos.CreateUpdateUserDto
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
            var newUserDto = new CreateUpdateUserDto
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
            _mockMapper.Setup(x => x.Map<global::BlossomAvenue.Service.UsersService.Dtos.UserDto>(newUserEntity)).Returns(savedUserDto);

            //Act
            var result = await _userManagement.CreateUser(newUserDto);

            //Assert
            _mockUserRepository.Verify(x => x.CreateUser(newUserEntity), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<global::BlossomAvenue.Service.UsersService.Dtos.UserDto>(result);
            Assert.Equal(Guid.Empty, newUserEntity.UserId);
            Assert.Equal("Admin", newUserEntity.UserRole.UserRoleName);
            Assert.True(newUserEntity.IsUserActive);
        }
        [Fact]
        public void UserManagement_ShouldHaveCreateProfileMethod()
        {
            //Arrange
            var type = typeof(UserManagement);

            // Act
            var createProfileMethod = type.GetMethod("CreateProfile");

            Assert.NotNull(createProfileMethod);
        }
        [Fact]
        public async Task UserManagement_CreateProfile_ShouldThrowExceptionIfEmailAlreadyExists()
        {
            //Arrange
            var newUserDto = new CreateDetailedUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "a.b@c.com"
            };

            _mockUserRepository.Setup(x => x.CheckUserExistsByEmail(newUserDto.Email)).ReturnsAsync(true);

            //Act and Assert
            await Assert.ThrowsAsync<RecordAlreadyExistsException>(() => _userManagement.CreateProfile(newUserDto));
        }
        [Fact]
        public async Task UserManagement_CreateProfile_ShouldThrowExceptionIfCityDoesNotExist()
        {
            //Arrange
            var newUserDto = new CreateDetailedUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "",
                CityId = Guid.NewGuid()
            };

            _mockUserRepository.Setup(x => x.CheckUserExistsByEmail(newUserDto.Email)).ReturnsAsync(false);
            _mockCityRepository.Setup(x => x.IsCityExists(newUserDto.CityId)).ReturnsAsync(false);

            //Act and Assert
            await Assert.ThrowsAsync<RecordNotFoundException>(() => _userManagement.CreateProfile(newUserDto));

        }

        [Fact]
        public async Task UserManagement_CreateProfile_ShouldCallCreateUserOnceAndValidReturn()
        {
            //Arrange
            var newUserDto = new CreateDetailedUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "",
                CityId = Guid.NewGuid()
            };

            var newUserEntity = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = ""
            };

            var savedUserDto = new CreateDetailedUserResponseDto
            {
                UserId = newUserEntity.UserId,
                FirstName = newUserEntity.FirstName,
                LastName = newUserEntity.LastName,
                Email = newUserEntity.Email
            };


            _mockUserRepository.Setup(x => x.CheckUserExistsByEmail(newUserDto.Email)).ReturnsAsync(false);
            _mockConfiguration.Setup(c => c.GetSection("UserRoles").GetSection("User").Value).Returns("Customer");
            _mockCityRepository.Setup(x => x.IsCityExists(newUserDto.CityId)).ReturnsAsync(true);
            _mockUserRoleRepository.Setup(x => x.GetUserRoleByName(It.IsAny<string>())).ReturnsAsync(new UserRole { UserRoleName = "Customer" });
            _mockMapper.Setup(x => x.Map<CreateDetailedUserResponseDto>(newUserDto)).Returns(savedUserDto);
            _mockMapper.Setup(x => x.Map<User>(newUserDto)).Returns(newUserEntity);
            _mockUserRepository.Setup(x => x.CreateUser(newUserEntity)).ReturnsAsync(newUserEntity);

            //Act
            var result = await _userManagement.CreateProfile(newUserDto);
            Assert.NotNull(result);
            Assert.IsType<CreateDetailedUserResponseDto>(result);
            Assert.NotEqual(Guid.Empty, newUserEntity.UserId);

        }
    }
}
