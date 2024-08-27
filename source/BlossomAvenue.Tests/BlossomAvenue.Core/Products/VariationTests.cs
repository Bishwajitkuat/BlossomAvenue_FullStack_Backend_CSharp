using System;
using BlossomAvenue.Core.Products;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Products
{
    public class VariationTests
    {
        [Fact]
        public void VariationClass_ShouldExists()
        {
            // Act
            var variationClassType = Type.GetType("BlossomAvenue.Core.Products.Variation, BlossomAvenue.Core");

            // Assert
            Assert.NotNull(variationClassType);
        }

        [Fact]
        public void Variation_ShouldHaveValidVariationId()
        {
            //Arrange
            var variation = typeof(Variation);

            // Act
            var variationId = variation.GetProperty("VariationId");

            // Assert
            Assert.NotNull(variationId);
            Assert.Equal(typeof(Guid), variationId.PropertyType);
        }

        [Fact]
        public void Variation_ShouldHaveValidVariationName()
        {
            //Arrange
            var variation = typeof(Variation);

            // Act
            var variationName = variation.GetProperty("VariationName");

            // Assert
            Assert.NotNull(variationName);
            Assert.Equal(typeof(string), variationName.PropertyType);
        }

        [Fact]
        public void Variation_ShouldHaveValidPrice()
        {
            //Arrange
            var variation = typeof(Variation);

            // Act
            var price = variation.GetProperty("Price");

            // Assert
            Assert.NotNull(price);
            Assert.Equal(typeof(decimal), price.PropertyType);
        }

        [Fact]
        public void Variation_ShouldHaveValidInventory()
        {
            //Arrange
            var variation = typeof(Variation);

            // Act
            var inventory = variation.GetProperty("Inventory");

            // Assert
            Assert.NotNull(inventory);
            Assert.Equal(typeof(int), inventory.PropertyType);
        }

        [Fact]
        public void Variation_ShouldHaveValidProductId()
        {
            //Arrange
            var variation = typeof(Variation);

            // Act
            var productId = variation.GetProperty("ProductId");

            // Assert
            Assert.NotNull(productId);
            Assert.Equal(typeof(Guid), productId.PropertyType);
        }







    }
}