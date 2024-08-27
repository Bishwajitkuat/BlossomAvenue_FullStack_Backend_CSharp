using System;
using BlossomAvenue.Core.Products;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Products
{
    public class ImageTests
    {
        [Fact]
        public void ImageClass_ShouldExists()
        {
            //Act
            var imageClassType = Type.GetType("BlossomAvenue.Core.Products.Image, BlossomAvenue.Core");

            //
            Assert.NotNull(imageClassType);
        }

        [Fact]
        public void Image_ShouldHaveValidImageId()
        {
            //Arrange
            var image = typeof(Image);

            // Act
            var imageId = image.GetProperty("ImageId");

            // Assert
            Assert.NotNull(imageId);
            Assert.Equal(typeof(Guid), imageId.PropertyType);
        }

        [Fact]
        public void Image_ShouldHaveValidImageUrl()
        {
            //Arrange
            var image = typeof(Image);

            // Act
            var imageUrl = image.GetProperty("ImageUrl");

            // Assert
            Assert.NotNull(imageUrl);
            Assert.Equal(typeof(string), imageUrl.PropertyType);
        }

        [Fact]
        public void Image_ShouldHaveValidProductId()
        {
            //Arrange
            var image = typeof(Image);

            // Act
            var productId = image.GetProperty("ProductId");

            // Assert
            Assert.NotNull(productId);
            Assert.Equal(typeof(Guid), productId.PropertyType);
        }
    }
}