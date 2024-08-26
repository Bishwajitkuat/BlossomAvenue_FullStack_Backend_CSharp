using System;
using Xunit;

using BlossomAvenue.Core.ProductAggregate;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Products
{
    public class ProductTests
    {
        [Fact]
        public void ProductClass_ShouldExists()
        {
            // Act
            var classType = Type.GetType("BlossomAvenue.Core.ProductAggregate.Product, BlossomAvenue.Core");

            // Assert
            Assert.NotNull(classType);
        }

        [Fact]
        public void Product_ShouldHaveValidProductId()
        {
            //Arrange
            var product = typeof(Product);

            // Act
            var productId = product.GetProperty("ProductId");

            // Assert
            Assert.NotNull(productId);
            Assert.Equal(typeof(Guid), productId.PropertyType);
        }

        [Fact]
        public void Product_ShouldHaveValidTitle()
        {
            //Arrange
            var product = typeof(Product);

            // Act
            var title = product.GetProperty("Title");


            // Assert
            Assert.NotNull(title);
            Assert.Equal(typeof(string), title.PropertyType);


        }

        [Fact]
        public void Product_ShouldHaveValidDescription()
        {
            //Arrange
            var product = typeof(Product);

            // Act
            var description = product.GetProperty("Description");

            // Assert
            Assert.NotNull(description);
            Assert.Equal(typeof(string), description.PropertyType);
        }
    }
}