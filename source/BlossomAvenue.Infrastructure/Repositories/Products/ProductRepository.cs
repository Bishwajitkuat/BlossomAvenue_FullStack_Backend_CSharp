using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastructure.Repositories.Products
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

        public async Task<bool> UpdateProduct(Guid productId, Product productToUpdate)
        {
            Product? product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (product == null) return false;
            // updating product
            product.Title = productToUpdate.Title;
            product.Description = productToUpdate.Description;
            product.Images = productToUpdate.Images;
            product.Variations = productToUpdate.Variations;
            product.ProductCategories = productToUpdate.ProductCategories;
            // saving context
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteProductById(Guid productId)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null) return false;
            _context.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Product>> GetAllProducts(ProductQueryDto pqdto)
        {
            var query = _context.Products
            .Include(p => p.Variations)
            .Include(p => p.Images)
            .Include(p => p.ProductReviews)
            .AsQueryable();

            if (!string.IsNullOrEmpty(pqdto.Search))
            {
                query = query.Where(p => p.Title.ToLower().Contains(pqdto.Search.ToLower()));
            }

            var isAscending = pqdto.OrderBy.ToString().ToLower().Equals("asc", StringComparison.OrdinalIgnoreCase);

            query = pqdto.ProductOrderWith switch
            {
                ProductOrderWith.Price => isAscending ? query.OrderBy(p => p.Variations.Min(a => a.Price)) : query.OrderByDescending(v => v.Variations.Max(a => a.Price)),
                ProductOrderWith.Title => isAscending ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title),
                ProductOrderWith.Inventory => isAscending ? query.OrderBy(p => p.Variations.Sum(v => v.Inventory)) : query.OrderByDescending(p => p.Variations.Sum(v => v.Inventory)),
                _ => query.OrderBy(p => p.Title)
            };

            var products = await query
            .Skip((pqdto.PageNo - 1) * pqdto.PageSize)
            .Take(pqdto.PageSize)
            .ToListAsync();
            return products;

        }
    }
}