using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Users
{
    public class UserCredentialEntityTests
    {
        [Fact]
        public void UserCredentialEntity_ShouldExists() 
        {
            //Act 
            var classType = Type.GetType("BlossomAvenue.Core.Users.UserCredential, BlossomAvenue.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void UserCredential_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(UserCredential);

            // Act
            var userId = type.GetProperty("UserId");
            var userName = type.GetProperty("UserName");
            var password = type.GetProperty("Password");
            var user = type.GetProperty("User");

            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid), userId.PropertyType);

            Assert.NotNull(userName);
            Assert.Equal(typeof(string), userName.PropertyType);

            Assert.NotNull(password);
            Assert.Equal(typeof(string), password.PropertyType);

            Assert.NotNull(user);
            Assert.Equal(typeof(User), user.PropertyType);
        }
    }
}
