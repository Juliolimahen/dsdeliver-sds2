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
                new ProductDTO
                {
                   Id = 5,
                   Name = "Risoto Funghi",
                   Price = 59.95,
                   Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                   ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
                },
            };

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            var controller = new ProductController(serviceMock.Object);

            // Act
            var result = await controller.FindAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<List<ProductDTO>>(okResult.Value);
            Assert.Equal(1, model.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResultWithProduct()
        {
            // Arrange
            var productId = 1;
            var product = new ProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(product);

            var controller = new ProductController(serviceMock.Object);

            // Act
            var result = await controller.GetByIdAsync(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(productId, model.Id);
        }

        [Fact]
        public async Task Insert_ReturnsCreatedResultWithProduct()
        {
            // Arrange
            var dto = new ProductDTO
            {
                Id = 5,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.InsertAsync(dto)).ReturnsAsync(dto);

            var controller = new ProductController(serviceMock.Object);

            // Act
            var result = await controller.InsertAsync(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<ProductDTO>(createdResult.Value);
            Assert.Equal(dto.Id, model.Id);
        }
    }
}
