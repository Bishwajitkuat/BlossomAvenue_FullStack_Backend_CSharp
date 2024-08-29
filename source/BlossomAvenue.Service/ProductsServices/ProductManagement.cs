using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Products;

namespace BlossomAvenue.Service.ProductsServices
{
    public class ProductManagement : IProductManagement
    {

        private IProductRepository _productRepository;

        public ProductManagement(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            var newProduct = await _productRepository.CreateProduct(product) ?? throw new RecordNotCreatedException("product");
            return newProduct;
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var product = await _productRepository.GetProductById(productId) ?? throw new RecordNotFoundException("product");
            return product;
        }
    }
}