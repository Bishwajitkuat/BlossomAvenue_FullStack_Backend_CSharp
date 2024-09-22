using BlossomAvenue.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Users
{
  public class AddressDetailEntityTests
  {
    [Fact]
    public void AddressDetailEntity_ShouldExists()
    {
      //Act 
      var classType = Type.GetType("BlossomAvenue.Core.Users.AddressDetail, BlossomAvenue.Core");

      //Assert
      Assert.NotNull(classType);
    }
    [Fact]
    public void AddressDetail_ShouldHaveValidProperties()
    {

      //Arrange
      var type = typeof(AddressDetail);

      // Act
      var addressDetailId = type.GetProperty("AddressDetailId");
      var addressLine1 = type.GetProperty("AddressLine1");
      var addressLine2 = type.GetProperty("AddressLine2");

      var city = type.GetProperty("City");



      Assert.Equal(typeof(Guid), addressDetailId.PropertyType);


      Assert.Equal(typeof(string), addressLine1.PropertyType);


      Assert.Equal(typeof(string), addressLine2.PropertyType);


      Assert.Equal(typeof(string), city.PropertyType);
    }
  }
}
