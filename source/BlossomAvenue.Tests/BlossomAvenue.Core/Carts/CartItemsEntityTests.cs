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
            var createdAt = type.GetProperty("CreatedAt");
            var cart = type.GetProperty("Cart");

            // Assert
            Assert.NotNull(cartItemsId);
            Assert.Equal(typeof(Guid),cartItemsId.PropertyType);

            Assert.NotNull(cartId);
            Assert.Equal(typeof(Guid),cartId.PropertyType);

            Assert.NotNull(productId);
            Assert.Equal(typeof(Guid),productId.PropertyType);

            Assert.NotNull(quantity);
            Assert.Equal(typeof(int?),quantity.PropertyType);

            Assert.NotNull(createdAt);
            Assert.Equal(typeof(DateTime?),createdAt.PropertyType);

            Assert.NotNull(cart);
            Assert.Equal(typeof(Cart),cart.PropertyType);
        }
    }
}