using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.ProductReviews;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.ProductReviewsService;
using BlossomAvenue.Service.Repositories.ProductReviews;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastructure.Repositories.ProductReviews
{
    public class ProductReviewsRepository : IProductReviewRepository
    {
        BlossomAvenueDbContext _context;
        public ProductReviewsRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateReview(ProductReviewCreateDto reviewCreateDto)
        {


            if (0 >= reviewCreateDto.Star && reviewCreateDto.Star > 5)
            {
                throw new ArgumentException("Rating should be 0 - 5");
            }

            // Create the new review
            var newReview = new ProductReview
            {
                ProductId = reviewCreateDto.ProductId,
                UserId = (Guid)reviewCreateDto.UserId,
                Review = reviewCreateDto.Review,
                Star = reviewCreateDto.Star,
            };

            _context.ProductReviews.Add(newReview);

            // Save the review to the database
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteReview(Guid reviewId)
        {
            var reviewItem = await _context.ProductReviews.FindAsync(reviewId);

            if (reviewItem == null)
            {
                return false;
            }

            _context.ProductReviews.Remove(reviewItem);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductReview>> GetReviewsByProduct(Guid productId)
        {
            return await _context.ProductReviews
                         .Where(review => review.ProductId == productId)
                         .ToListAsync();
        }

        public async Task<List<ProductReview>> GetReviewsByUser(Guid userId)
        {
            return await _context.ProductReviews
                         .Where(review => review.UserId == userId)
                         .ToListAsync();
        }

        public async Task<ProductReview> GetSingleReview(Guid reviewId)
        {
            return await _context.ProductReviews
                         .FindAsync(reviewId);
        }

        public async Task<bool> UpdateReview(Guid reviewId, string review, int star)
        {
            var updateReview = await _context.ProductReviews.FindAsync(reviewId);

            if (updateReview == null)
            {
                throw new RecordNotFoundException("productReview");
            }

            updateReview.Review = review;
            updateReview.Star = star;

            _context.ProductReviews.Update(updateReview);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}