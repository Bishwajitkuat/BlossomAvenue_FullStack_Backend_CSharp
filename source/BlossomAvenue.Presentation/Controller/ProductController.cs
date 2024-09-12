using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.Repositories.Products;
using BlossomAvenue.Service.SharedDtos;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost("")]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto createProductDto)
        {
            var product = createProductDto.ConvertToProduct();
            var newProduct = await _productManagement.CreateProduct(product);
            Response.StatusCode = 201;
            return Created(nameof(CreateProduct), newProduct);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetProductByIdReadDto>> GetProductById([FromRoute] Guid id)
        {
            var product = await _productManagement.GetProductById(id);
            var readProduct = new GetProductByIdReadDto(product);
            return Ok(readProduct);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPatch("{id:Guid}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDto updateProductDto)
        {

            await _productManagement.UpdateProduct(id, updateProductDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] Guid id)
        {
            await _productManagement.DeleteProductById(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GetAllProductReadDto>>> GetAllProducts([FromQuery] ProductQueryDto pqdto)
        {
            var paginatedProducts = await _productManagement.GetAllProducts(pqdto);
            var readProducts = paginatedProducts.Items.Select(p => new GetAllProductReadDto(p)).ToList();
            var readPaginatedProducts = new PaginatedResponse<GetAllProductReadDto>(readProducts, paginatedProducts.ItemPerPage, paginatedProducts.CurrentPage, paginatedProducts.TotalItemCount);
            return Ok(readPaginatedProducts);
        }







    }











}