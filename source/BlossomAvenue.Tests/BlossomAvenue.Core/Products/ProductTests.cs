using System;
using System.Collections.Generic;
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

        [Fact]
        public void Product_ShouldHaveValidImages()
        {
            //Arrange
            var product = typeof(Product);

            // Act
            var images = product.GetProperty("Images");

            // Assert
            Assert.NotNull(images);
            Assert.Equal(typeof(IEnumerable<IImage>), images.PropertyType);
        }

        [Fact]
        public void Product_ShouldHaveValidVariations()
        {
            //Arrange
            var product = typeof(Product);

            // Act
            var variations = product.GetProperty("Variations");

            // Assert
            Assert.NotNull(variations);
            Assert.Equal(typeof(IEnumerable<IVariation>), variations.PropertyType);
        }

        [Fact]
        public void Product_ShouldHaveValidCategories()
        {
            //Arrange
            var product = typeof(Product);

            // Act
            var categories = product.GetProperty("Categories");

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(typeof(IEnumerable<ICategory>), categories.PropertyType);
        }
    }
}