using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.SharedDtos;

namespace BlossomAvenue.Service.Repositories.Products
{
    public interface IProductRepository
    {
        public Task<Product?> CreateProduct(Product product);
        public Task<Product> UpdateProduct(Product productToUpdate);
        public Task<Product?> GetProductById(Guid productId);

        public Task<bool> DeleteProductById(Guid productId);

        public Task<PaginatedResponse<Product>> GetAllProducts(ProductQueryDto pqdto);
    }
}