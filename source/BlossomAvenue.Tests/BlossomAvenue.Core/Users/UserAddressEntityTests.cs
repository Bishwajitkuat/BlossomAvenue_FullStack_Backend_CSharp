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
            var addressId = type.GetProperty("AddressId");
            var defaultAddress = type.GetProperty("DefaultAddress");
            var address = type.GetProperty("Address");
            var user = type.GetProperty("User");

            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid), userId.PropertyType);

            Assert.NotNull(addressId);
            Assert.Equal(typeof(Guid), addressId.PropertyType);

            Assert.NotNull(defaultAddress);
            Assert.Equal(typeof(bool?), defaultAddress.PropertyType);

            Assert.NotNull(address);
            Assert.Equal(typeof(AddressDetail), address.PropertyType);

            Assert.NotNull(user);
            Assert.Equal(typeof(User), user.PropertyType);
        }
    }
}
