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

        public async Task<Product?> GetProductById(Guid productId)
        {
            var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Variations)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductReviews)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product is null) return null;
            return product;
        }

        public Task<bool> UpdateProduct(Guid productId, UpdateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}