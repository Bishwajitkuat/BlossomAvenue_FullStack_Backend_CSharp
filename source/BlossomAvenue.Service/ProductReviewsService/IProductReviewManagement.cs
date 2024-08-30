using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.ProductReviews;

namespace BlossomAvenue.Service.ProductReviewsService
{
    public interface IProductReviewManagement
    {
        public Task<bool> CreateReview(ProductReviewCreateDto reviewCreateDto);
        public Task<bool> UpdateReview(Guid reviewId, string review, int star);
        public Task<bool> DeleteReview(Guid reviewId);
        public Task<ProductReview> GetSingleReview(Guid reviewId);
        public Task<List<ProductReview>> GetReviewsByProduct(Guid productId);
        public Task<List<ProductReview>> GetReviewsByUser(Guid userId);

    }
}