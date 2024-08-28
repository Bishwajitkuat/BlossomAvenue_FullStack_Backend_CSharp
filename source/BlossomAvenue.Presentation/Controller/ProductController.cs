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
            return product;
        }








    }











}