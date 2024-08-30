using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.ProductReviewsService;
using Microsoft.AspNetCore.Mvc;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductReviewController : ControllerBase
    {
        IProductReviewManagement _productReviewManagement;
        public ProductReviewController(IProductReviewManagement productReviewManagement)
        {
            _productReviewManagement = productReviewManagement;
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview(ProductReviewCreateDto reviewCreateDto)
        {
            try
            {
                var success = await _productReviewManagement.CreateReview(reviewCreateDto);

                if (success)
                {
                    return Ok(new { Message = "Review posted successfully." });
                }
                else
                {
                    throw new Exception("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateRevirew(Guid reviewId, string review, int star)
        {
            try
            {
                var success = await _productReviewManagement.UpdateReview(reviewId, review, star);

                if (success)
                {
                    return Ok(new { Message = "Review updated successfully." });
                }
                else
                {
                    throw new Exception("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("productId")]
        public async Task<IActionResult> GetReviewsByProduct(Guid productId)
        {
            try
            {
                var reviews = await _productReviewManagement.GetReviewsByProduct(productId);

                if (reviews != null)
                {
                    return Ok(reviews);
                }
                else
                {
                    throw new RecordNotFoundException("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetReviewsByUser(Guid userId)
        {
            try
            {
                var reviews = await _productReviewManagement.GetReviewsByUser(userId);

                if (reviews != null)
                {
                    return Ok(reviews);
                }
                else
                {
                    throw new RecordNotFoundException("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("reviewId")]
        public async Task<IActionResult> GetSingleReview(Guid reviewId)
        {
            try
            {
                var review = await _productReviewManagement.GetSingleReview(reviewId);

                if (review != null)
                {
                    return Ok(review);
                }
                else
                {
                    throw new RecordNotFoundException("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}