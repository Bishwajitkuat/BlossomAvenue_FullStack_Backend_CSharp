using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Users
{

  public class UserEntityTests
  {
    [Fact]
    public void UsersEntity_ShouldExists()
    {
      //Act 
      var classType = Type.GetType("BlossomAvenue.Core.Users.User, BlossomAvenue.Core");

      //Assert
      Assert.NotNull(classType);
    }

    [Fact]
    public void Users_ShouldHaveValidProperties()
    {

      //Arrange
      var type = typeof(User);

      // Act
      var userId = type.GetProperty("UserId");
      var firstName = type.GetProperty("FirstName");
      var lastName = type.GetProperty("LastName");
      var email = type.GetProperty("Email");
      var userRoleId = type.GetProperty("UserRoleId");
      var lastLogin = type.GetProperty("LastLogin");
      var isUserActive = type.GetProperty("IsUserActive");
      var createdAt = type.GetProperty("CreatedAt");
      var userAddresses = type.GetProperty("UserAddresses");


      // Assert

      Assert.Equal(typeof(Guid), userId.PropertyType);


      Assert.Equal(typeof(string), firstName.PropertyType);


      Assert.Equal(typeof(string), lastName.PropertyType);


      Assert.Equal(typeof(string), email.PropertyType);



      Assert.Equal(typeof(DateTime?), lastLogin.PropertyType);


      Assert.Equal(typeof(bool?), isUserActive.PropertyType);


      Assert.Equal(typeof(DateTime?), createdAt.PropertyType);


      Assert.Equal(typeof(ICollection<UserAddress>), userAddresses.PropertyType);


    }
  }
}
