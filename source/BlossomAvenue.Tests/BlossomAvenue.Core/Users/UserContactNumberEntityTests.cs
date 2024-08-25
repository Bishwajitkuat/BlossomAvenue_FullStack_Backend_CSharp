using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Users
{
    public class UserContactNumberEntityTests
    {
        [Fact]
        public void UserContactNumberEntity_ShouldExists() 
        {
            //Act 
            var classType = Type.GetType("BlossomAvenue.Core.Users.UserContactNumber, BlossomAvenue.Core");

            //Assert
            Assert.NotNull(classType);
        }
        [Fact]
        public void UserContactNumber_ShouldHaveValidProperties()
        {

            //Arrange
            var type = typeof(UserContactNumber);

            // Act
            var userId = type.GetProperty("UserId");
            var contactNumber = type.GetProperty("ContactNumber");
            var user = type.GetProperty("User");

            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid), userId.PropertyType);

            Assert.NotNull(contactNumber);
            Assert.Equal(typeof(string), contactNumber.PropertyType);

            Assert.NotNull(user);
            Assert.Equal(typeof(User), user.PropertyType);
        }
    }
}
