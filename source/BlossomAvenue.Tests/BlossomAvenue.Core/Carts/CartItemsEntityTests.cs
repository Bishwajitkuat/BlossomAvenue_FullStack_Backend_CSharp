using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Carts
{
    public class CartItemEntityTests
    {
        [Fact]
        public void CartItemsEntity_ShouldExists()
        {
            //Act 
            var classType = Type.GetType("BlossomAvenue.Core.Carts.CartItem, BlossomAvenue.Core");

            //Assert
            Assert.NotNull(classType);
        }

        [Fact]
        public void CartItems_ShouldHaveValidproperties()
        {
            //Arrange
            var type = typeof(CartItem);

            // Act
            var cartItemsId = type.GetProperty("CartItemsId");
            var cartId = type.GetProperty("CartId");
            var productId = type.GetProperty("ProductId");
            var quantity = type.GetProperty("Quantity");


            // Assert

            Assert.Equal(typeof(Guid), cartItemsId.PropertyType);
            Assert.Equal(typeof(Guid), cartId.PropertyType);
            Assert.Equal(typeof(Guid), productId.PropertyType);
            Assert.Equal(typeof(int), quantity.PropertyType);

        }
    }
}