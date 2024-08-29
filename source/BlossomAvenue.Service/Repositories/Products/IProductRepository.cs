using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.ProductsServices;

namespace BlossomAvenue.Service.Repositories.Products
{
    public interface IProductRepository
    {
        public Task<Product?> CreateProduct(Product product);
        public Task<bool> UpdateProduct(Guid productId, Product productToUpdate);
        public Task<Product?> GetProductById(Guid productId);

        public Task<bool> DeleteProductById(Guid productId);

        public Task<ICollection<Product>> GetAllProducts(ProductQueryDto pqdto);
    }
}