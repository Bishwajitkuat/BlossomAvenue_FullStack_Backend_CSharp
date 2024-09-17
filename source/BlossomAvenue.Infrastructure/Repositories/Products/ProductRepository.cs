using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.Repositories.Products;
using BlossomAvenue.Service.SharedDtos;
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
            if (await _context.SaveChangesAsync() > 0)
            {
                return newProduct;
            }
            return null;
        }



        public async Task<Product?> GetProductById(Guid productId)
        {
            var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Variations)
            .Include(p => p.ProductCategories)
            .ThenInclude(p => p.Category)
            .Include(p => p.ProductReviews)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product is null) return null;
            return product;
        }

        public async Task<Product> UpdateProduct(Product updatedProduct)
        {
            _context.Products.Update(updatedProduct);
            if (await _context.SaveChangesAsync() > 0) return updatedProduct;
            else throw new RecordNotUpdatedException("product");
        }


        public async Task<bool> DeleteProductById(Guid productId)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null) return false;
            _context.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResponse<Product>> GetAllProducts(ProductQueryDto pqdto)
        {
            var query = _context.Products
            .Include(p => p.Variations.OrderBy(v => v.Price).Take(1))
            .Include(p => p.Images.Take(1))
            .Include(p => p.ProductReviews)
            .AsSplitQuery()
            .AsQueryable();

            if (!string.IsNullOrEmpty(pqdto.Search))
            {
                query = query.Where(p => p.Title.ToLower().Contains(pqdto.Search.ToLower()));
            }

            if (pqdto.CategoryId is not null)
            {
                query = query.Where(p => p.ProductCategories.Where(c => c.CategoryId == pqdto.CategoryId).Count() > 0);
            }

            // total count after the filtering
            var totalItemCount = await query.CountAsync();

            if (pqdto.PageNo > 1 && (pqdto.PageNo * pqdto.PageSize) > totalItemCount)
            {
                pqdto.PageNo = 1;
            }

            var isAscending = pqdto.OrderBy.ToString().ToLower().Equals("asc", StringComparison.OrdinalIgnoreCase);

            query = pqdto.ProductOrderWith switch
            {
                ProductOrderWith.Price => isAscending ? query.OrderBy(p => p.Variations.Min(a => a.Price)) : query.OrderByDescending(v => v.Variations.Min(a => a.Price)),
                ProductOrderWith.Title => isAscending ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title),
                ProductOrderWith.Inventory => isAscending ? query.OrderBy(p => p.Variations.Sum(v => v.Inventory)) : query.OrderByDescending(p => p.Variations.Sum(v => v.Inventory)),
                ProductOrderWith.CreatedAt => isAscending ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
                _ => isAscending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt)
            };

            var products = await query
            .Skip((pqdto.PageNo - 1) * pqdto.PageSize)
            .Take(pqdto.PageSize)
            .ToListAsync();


            return new PaginatedResponse<Product>(products, pqdto.PageSize, pqdto.PageNo, totalItemCount);

        }
    }
}