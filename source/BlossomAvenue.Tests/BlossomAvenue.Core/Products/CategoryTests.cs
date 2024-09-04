using System;
using BlossomAvenue.Core.Products;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.Products
{
    public class CategoryTests
    {
        [Fact]
        public void CategoryClass_ShouldExists()
        {
            // Act
            var categoryClassType = Type.GetType("BlossomAvenue.Core.Products.Category, BlossomAvenue.Core");

            // Assert
            Assert.NotNull(categoryClassType);
        }

        [Fact]
        public void Category_ShouldHaveValidCategoryId()
        {
            //Arrange
            var category = typeof(Category);

            // Act
            var categoryId = category.GetProperty("CategoryId");

            // Assert
            Assert.NotNull(categoryId);
            Assert.Equal(typeof(Guid), categoryId.PropertyType);
        }

        [Fact]
        public void Category_ShouldHaveValidCategoryName()
        {
            //Arrange
            var category = typeof(Category);

            // Act
            var categoryName = category.GetProperty("CategoryName");

            // Assert
            Assert.NotNull(categoryName);
            Assert.Equal(typeof(string), categoryName.PropertyType);
        }

        [Fact]
        public void Category_ShouldHaveValidParentId()
        {
            //Arrange
            var category = typeof(Category);

            // Act
            var parentId = category.GetProperty("ParentId");

            // Assert
            Assert.NotNull(parentId);
            Assert.Equal(typeof(Guid?), parentId.PropertyType);
        }
    }
}