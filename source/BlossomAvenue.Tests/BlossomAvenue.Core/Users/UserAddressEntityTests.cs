using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Users
{
  public class UserAddressEntityTests
  {
    [Fact]
    public void UserAddressEntity_ShouldExists()
    {
      //Act 
      var classType = Type.GetType("BlossomAvenue.Core.Users.UserAddress, BlossomAvenue.Core");

      //Assert
      Assert.NotNull(classType);
    }
    [Fact]
    public void UserAddress_ShouldHaveValidProperties()
    {

      //Arrange
      var type = typeof(UserAddress);

      // Act
      var userId = type.GetProperty("UserId");
      var userAddressId = type.GetProperty("UserAddressId");
      var defaultAddress = type.GetProperty("DefaultAddress");
      var addressDetail = type.GetProperty("AddressDetail");
      var user = type.GetProperty("User");


      Assert.Equal(typeof(Guid), userId.PropertyType);


      Assert.Equal(typeof(Guid), userAddressId.PropertyType);


      Assert.Equal(typeof(bool?), defaultAddress.PropertyType);


      Assert.Equal(typeof(AddressDetail), addressDetail.PropertyType);


      Assert.Equal(typeof(User), user.PropertyType);
    }
  }
}
