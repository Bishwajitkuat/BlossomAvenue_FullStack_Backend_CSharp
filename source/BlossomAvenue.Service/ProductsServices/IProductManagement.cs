using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.SharedDtos;

namespace BlossomAvenue.Service.ProductsServices
{
    public interface IProductManagement
    {
        public Task<Product> CreateProduct(Product product);
        public Task<Product> GetProductById(Guid productId);

        public Task<Product> UpdateProduct(Guid productId, UpdateProductDto productToUpdate);
        public Task<bool> DeleteProductById(Guid productId);

        public Task<PaginatedResponse<Product>> GetAllProducts(ProductQueryDto pqdto);
    }
}