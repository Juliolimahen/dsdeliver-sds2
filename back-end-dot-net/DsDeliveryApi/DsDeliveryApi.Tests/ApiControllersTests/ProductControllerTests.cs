using DsDelivery.Core.Shared;
using DsDelivery.Manager.Interfaces;
using DsDelivery.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DsDeliveryApi.Tests.ApiControllersTests
{
    public class ProductControllerTests
    {
        [Fact] 
        public async Task FindAll_ReturnsOkResultWithListOfProducts()
        {
            // Arrange
            var products = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, Name = "Product 1"},
                new ProductDTO { Id = 2, Name = "Product 2"}
            };

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            var controller = new ProductController(serviceMock.Object);

            // Act
            var result = await controller.FindAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<List<ProductDTO>>(okResult.Value);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResultWithProduct()
        {
            // Arrange
            var productId = 1;
            var product = new ProductDTO { Id = productId, Name = "Product 1"};

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(product);

            var controller = new ProductController(serviceMock.Object);

            // Act
            var result = await controller.GetByIdAsync(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.ExecuteResultAsync);
            var model = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(productId, model.Id);
        }

        [Fact]
        public async Task Insert_ReturnsCreatedResultWithProduct()
        {
            // Arrange
            var dto = new ProductDTO { Name = "Product 1"};

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.InsertAsync(dto)).ReturnsAsync(dto);

            var controller = new ProductController(serviceMock.Object);

            // Act
            var result = await controller.InsertAsync(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.ExecuteResultAsync);
            var model = Assert.IsType<ProductDTO>(createdResult.Value);
            Assert.Equal(dto.Id, model.Id);
        }
    }
}
