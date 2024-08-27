using AutoMapper;
using BlossomAvenue.Core.Repositories.Users;
using BlossomAvenue.Core.Users;
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
        public async Task GetUser_ShouldReturnUserDtoList() 
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
    }
}
