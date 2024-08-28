using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.Repositories.Products;
using Moq;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Service.Products
{
    public class ProductManagementTests
    {

        private IProductManagement _productMg;
        private Mock<IProductRepository> _mockProductRepo;

        public ProductManagementTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _productMg = new ProductManagement(_mockProductRepo.Object);
        }


        [Fact]
        public async Task CreateProduct_ValidData_ReturnProduct()
        {
            // Arrange
            var mockProduct = new Mock<Product>().Object;
            _mockProductRepo.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(mockProduct);

            // Act
            var result = await _productMg.CreateProduct(mockProduct);

            //Assert
            Assert.Equal(result, mockProduct);
        }

        [Fact]
        public async Task CreateProduct_InValidData_RiseException()
        {
            // Arrange
            var mockProduct = new Mock<Product>().Object;
            Product? nullProduct = null;
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(nullProduct);

            // Act and Assert
            await Assert.ThrowsAsync<RecordNotCreatedException>(() =>
            _productMg.CreateProduct(mockProduct)
            );
        }


        [Fact]
        public async Task GetProductById_ValidData_ReturnGetProductByIdDto()
        {
            // Arrange
            var mockGetProductByIdDto = new Mock<GetProductByIdReadDto>().Object;
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.GetProductById(It.IsAny<Guid>())).ReturnsAsync(mockGetProductByIdDto);

            // Act
            var result = await _productMg.GetProductById(id);

            //Assert
            Assert.Equal(result, mockGetProductByIdDto);
        }


        [Fact]
        public async Task GetProductById_InValidData_RiseException()
        {
            // Arrange
            GetProductByIdReadDto? mockGetProductByIdDto = null;
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.GetProductById(It.IsAny<Guid>())).ReturnsAsync(mockGetProductByIdDto);

            // Act and Assert
            await Assert.ThrowsAsync<RecordNotFoundException>(() =>
            _productMg.GetProductById(id)
            );
        }
    }
}