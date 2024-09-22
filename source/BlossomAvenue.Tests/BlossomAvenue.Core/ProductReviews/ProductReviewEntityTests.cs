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
      var review = type.GetProperty("Review");
      var star = type.GetProperty("Star");


      // Assert

      Assert.Equal(typeof(Guid), userId.PropertyType);

      Assert.Equal(typeof(Guid), productId.PropertyType);


      Assert.Equal(typeof(Guid), reviewId.PropertyType);


      Assert.Equal(typeof(string), review.PropertyType);


      Assert.Equal(typeof(int), star.PropertyType);
    }
  }
}