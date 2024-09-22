using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Orders
{
  public class OrderItemEntityTests
  {
    [Fact]
    public void OrderItemsEntity_ShouldExists()
    {
      //Act 
      var classType = Type.GetType("BlossomAvenue.Core.Orders.OrderItem, BlossomAvenue.Core");

      //Assert
      Assert.NotNull(classType);
    }

    [Fact]
    public void OrderItems_ShouldHaveValidproperties()
    {
      //Arrange
      var type = typeof(OrderItem);

      // Act
      var orderItemsId = type.GetProperty("OrderItemsId");
      var orderId = type.GetProperty("OrderId");
      var productId = type.GetProperty("ProductId");
      var quantity = type.GetProperty("Quantity");
      var price = type.GetProperty("Price");
      var order = type.GetProperty("Order");

      // Assert

      Assert.Equal(typeof(Guid), orderItemsId.PropertyType);


      Assert.Equal(typeof(Guid), orderId.PropertyType);


      Assert.Equal(typeof(Guid), productId.PropertyType);


      Assert.Equal(typeof(int), quantity.PropertyType);


      Assert.Equal(typeof(decimal), price.PropertyType);
      Assert.Equal(typeof(Order), order.PropertyType);
    }
  }
}