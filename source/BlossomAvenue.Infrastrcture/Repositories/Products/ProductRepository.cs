using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastrcture.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private BlossomAvenueDbContext _context;

        public ProductRepository(BlossomAvenueDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<Product?> CreateProduct(Product product)
        {
            var newProduct = (await _context.Products.AddAsync(product)).Entity;
            _context.SaveChanges();
            return newProduct;
        }

        public async Task<GetProductByIdReadDto?> GetProductById(Guid productId)
        {
            var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Variations)
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product is null) return null;

            var categories = await _context.ProductCategories
            .Where(pc => pc.ProductId == productId)
            .Join(_context.Categories,
            pc => pc.CategoryId,
            c => c.CategoryId,
            (pc, c) => new Category
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                ParentId = c.ParentId
            }
            ).ToListAsync();

            var productReviews = await _context.ProductReviews.Where(p => p.ProductId == productId).ToListAsync();

            var stars = await _context.ProductReviews.Where(p => p.ProductId == productId && p.Star != null).Select(r => r.Star).ToListAsync();
            decimal avgStar = 0;
            if (stars.Count > 1)
            {
                avgStar = (decimal)stars.Average();
            }

            return new GetProductByIdReadDto
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Images = product.Images,
                Variations = product.Variations,
                Categories = categories,
                ProductReviews = productReviews,
                AvgStar = avgStar
            };
        }

        public Task<bool> UpdateProduct(Guid productId, UpdateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}