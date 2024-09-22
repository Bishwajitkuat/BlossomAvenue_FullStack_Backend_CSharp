using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ProductReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Orders
{
  public class OrderEntityTests
  {
    [Fact]
    public void OrdersEntity_ShouldExists()
    {
      //Act 
      var classType = Type.GetType("BlossomAvenue.Core.Orders.Order, BlossomAvenue.Core");

      //Assert
      Assert.NotNull(classType);
    }

    [Fact]
    public void Orders_ShouldHaveValidProperties()
    {

      //Arrange
      var type = typeof(Order);

      // Act
      var orderId = type.GetProperty("OrderId");
      var userId = type.GetProperty("UserId");
      var addressId = type.GetProperty("AddressId");
      var totalAmount = type.GetProperty("TotalAmount");
      var createdAt = type.GetProperty("CreatedAt");
      var orderItems = type.GetProperty("OrderItems");


      // Assert

      Assert.Equal(typeof(Guid), userId.PropertyType);

      Assert.Equal(typeof(decimal), totalAmount.PropertyType);

      Assert.Equal(typeof(DateTime?), createdAt.PropertyType);

      Assert.Equal(typeof(ICollection<OrderItem>), orderItems.PropertyType);
    }
  }
}