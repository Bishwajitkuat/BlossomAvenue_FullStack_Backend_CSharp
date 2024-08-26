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
            var addressDetailId = type.GetProperty("AddressId");
            var addressLine1 = type.GetProperty("AddressLine1");
            var addressLine2 = type.GetProperty("AddressLine2");
            var cityId = type.GetProperty("CityId");
            var city = type.GetProperty("City");
            var userAddresses = type.GetProperty("UserAddresses");

            Assert.NotNull(addressDetailId);
            Assert.Equal(typeof(Guid), addressDetailId.PropertyType);

            Assert.NotNull(addressLine1);
            Assert.Equal(typeof(string), addressLine1.PropertyType);

            Assert.NotNull(addressLine2);
            Assert.Equal(typeof(string), addressLine2.PropertyType);

            Assert.NotNull(cityId);
            Assert.Equal(typeof(Guid?), cityId.PropertyType);

            Assert.NotNull(city);
            Assert.Equal(typeof(City), city.PropertyType);

            Assert.NotNull(userAddresses);
            Assert.Equal(typeof(ICollection<UserAddress>), userAddresses.PropertyType);
        }
    }
}
