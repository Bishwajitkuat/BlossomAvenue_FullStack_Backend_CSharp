using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.ProductReviews;

namespace BlossomAvenue.Service.ProductsServices
{
    public class ReadProductReviewDto
    {
        public Guid ReviewId { get; set; }

        public string? Review { get; set; }

        public int? Star { get; set; }

        public string UserName { get; set; }

        public ReadProductReviewDto(ProductReview pr)
        {
            ReviewId = pr.ReviewId;
            Review = pr.Review;
            Star = pr.Star;
            UserName = pr.User.FirstName + " " + pr.User.LastName;
        }


    }
}