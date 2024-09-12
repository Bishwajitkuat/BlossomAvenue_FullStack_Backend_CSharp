using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Products;
using BlossomAvenue.Service.SharedDtos;

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
            var product = await _productRepository.GetProductById(productId);
            if (product == null) throw new RecordNotFoundException("product");
            return product;
        }

        public async Task<bool> UpdateProduct(Guid productId, UpdateProductDto productToUpdate)
        {
            var oldProduct = await _productRepository.GetProductById(productId);
            if (oldProduct == null) throw new RecordNotFoundException("product");
            var updatedProduct = productToUpdate.UpdateProduct(oldProduct);
            bool result = await _productRepository.UpdateProduct(updatedProduct);

            if (result != true) throw new RecordNotFoundException("product");
            return result;
        }


        public async Task<bool> DeleteProductById(Guid productId)
        {

            var result = await _productRepository.DeleteProductById(productId);
            if (result != true) throw new RecordNotFoundException("product");
            return result;
        }

        public async Task<PaginatedResponse<Product>> GetAllProducts(ProductQueryDto pqdto)
        {
            return await _productRepository.GetAllProducts(pqdto);
        }
    }
}