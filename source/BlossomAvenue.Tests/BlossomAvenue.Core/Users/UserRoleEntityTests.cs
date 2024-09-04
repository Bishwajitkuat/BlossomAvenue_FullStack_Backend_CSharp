using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Users
{
    public  class UserRoleEntityTests
    {
        [Fact]
        public void UserRoleEntity_ShouldExists() 
        {
            //Act 
            var classType = Type.GetType("BlossomAvenue.Core.Users.UserRole, BlossomAvenue.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void UserRole_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(UserRole);

            // Act
            var userRoleId = type.GetProperty("UserRoleId");
            var userRoleName = type.GetProperty("UserRoleName");
            var users = type.GetProperty("Users");

            Assert.NotNull(userRoleId);
            Assert.Equal(typeof(Guid), userRoleId.PropertyType);

            Assert.NotNull(userRoleName);
            Assert.Equal(typeof(string), userRoleName.PropertyType);

            Assert.NotNull(users);
            Assert.Equal(typeof(ICollection<User>), users.PropertyType);
        }
    }
}
