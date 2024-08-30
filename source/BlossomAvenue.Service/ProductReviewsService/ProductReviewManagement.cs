using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.ProductReviews;
using BlossomAvenue.Service.Repositories.ProductReviews;

namespace BlossomAvenue.Service.ProductReviewsService
{
    public class ProductReviewManagement : IProductReviewManagement
    {
        private IProductReviewRepository _productReviewRepository;
        public ProductReviewManagement(IProductReviewRepository productReviewRepository)
        {
            _productReviewRepository = productReviewRepository;
        }
        public async Task<bool> CreateReview(ProductReviewCreateDto reviewCreateDto)
        {
            return await _productReviewRepository.CreateReview(reviewCreateDto);
        }

        public async Task<bool> DeleteReview(Guid reviewId)
        {
            return await _productReviewRepository.DeleteReview(reviewId);
        }

        public async Task<List<ProductReview>> GetReviewsByProduct(Guid productId)
        {
            return await _productReviewRepository.GetReviewsByProduct(productId);
        }

        public async Task<List<ProductReview>> GetReviewsByUser(Guid userId)
        {
            return await _productReviewRepository.GetReviewsByProduct(userId);
        }

        public async Task<ProductReview> GetSingleReview(Guid reviewId)
        {
            return await _productReviewRepository.GetSingleReview(reviewId);
        }

        public async Task<bool> UpdateReview(Guid reviewId, string review, int star)
        {
            return await _productReviewRepository.UpdateReview(reviewId, review, star);
        }
    }
}