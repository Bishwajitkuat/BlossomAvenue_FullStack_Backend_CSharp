using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ProductReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Tests.BlossomAvenue.Core.ProductReviews
{
    public class ProductReviewsEntityTests
    {
        [Fact]
        public void ProductReviewsEntity_ShouldExists()
        {
            //Act 
            var classType = Type.GetType("BlossomAvenue.Core.ProductReviews.ProductReview, BlossomAvenue.Core");

            //Assert
            Assert.NotNull(classType);
        }

        [Fact]
        public void ProductReviews_ShouldHaveValidProperties() 
        {

            //Arrange
            var type = typeof(ProductReview);

            // Act
            var reviewId = type.GetProperty("ReviewId");
            var userId = type.GetProperty("UserId");
            var productId = type.GetProperty("ProductId");
            var orderId = type.GetProperty("OrderId");
            var review = type.GetProperty("Review");
            var star = type.GetProperty("Star");
            var order = type.GetProperty("Order");

            // Assert
            Assert.NotNull(userId);
            Assert.Equal(typeof(Guid),userId.PropertyType);

            Assert.NotNull(orderId);
            Assert.Equal(typeof(Guid),orderId.PropertyType);

            Assert.NotNull(reviewId);
            Assert.Equal(typeof(Guid),reviewId.PropertyType);

            Assert.NotNull(review);
            Assert.Equal(typeof(string),review.PropertyType);

            Assert.NotNull(star);
            Assert.Equal(typeof(int?),star.PropertyType);
            
            Assert.NotNull(order);
            Assert.Equal(typeof(Order),order.PropertyType);
        }
    }
}