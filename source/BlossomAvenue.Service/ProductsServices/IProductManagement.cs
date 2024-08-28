using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Service.ProductsServices
{
    public interface IProductManagement
    {
        public Task<Product> CreateProduct(Product product);
        public Task<GetProductByIdReadDto> GetProductById(Guid productId);
    }
}