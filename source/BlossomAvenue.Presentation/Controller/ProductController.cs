using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.Repositories.Products;
using Microsoft.AspNetCore.Mvc;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class ProductController : ControllerBase
    {
        private IProductManagement _productManagement;

        public ProductController(IProductManagement productManagement)
        {
            _productManagement = productManagement;
        }

        // ADMIN USER
        [HttpPost("")]
        public async Task<Product> CreateProduct(CreateProductDto createProductDto)
        {
            var product = createProductDto.ConvertToProduct();
            var newProduct = await _productManagement.CreateProduct(product);
            return newProduct;

        }

        [HttpGet("{id:Guid}")]
        public async Task<GetProductByIdReadDto> GetProductById([FromRoute] Guid id)
        {
            var product = await _productManagement.GetProductById(id);
            var readProduct = new GetProductByIdReadDto(product);
            return readProduct;
        }

        [HttpPatch("{id:Guid}")]
        public async Task<bool> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDto updateProductDto)
        {

            var productToUpdate = updateProductDto.ConvertToProduct();
            return await _productManagement.UpdateProduct(id, productToUpdate);
        }


        [HttpDelete("{id:Guid}")]
        public async Task<bool> DeleteProductById([FromRoute] Guid id)
        {
            return await _productManagement.DeleteProductById(id);
        }







    }











}