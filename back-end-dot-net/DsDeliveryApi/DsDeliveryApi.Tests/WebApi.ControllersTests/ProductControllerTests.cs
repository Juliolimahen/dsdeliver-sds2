using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Manager.Interfaces;
using DsDelivery.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DsDelivery.WebApi.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;
        private readonly Mock<IProductService> _serviceMock;

        public ProductControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsOkResultWithData()
        {
            // Arrange
            int productId = 1;

            var productDto = new ProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(productDto);

            // Act
            var result = await _controller.GetByIdAsync(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(productDto, model);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int productId = 2003;
            _serviceMock.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync((ProductDTO)null);

            // Act
            var result = await _controller.GetByIdAsync(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task InsertAsync_WithValidDto_ReturnsOkResultWithData()
        {
            // Arrange
            var productDto = new ProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.InsertAsync(It.IsAny<ProductDTO>())).ReturnsAsync(productDto);

            // Act
            var result = await _controller.Insert(productDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(productDto, model);
        }

        [Fact]
        public async Task InsertAsync_WithInvalidDto_ReturnsInternalServerError()
        {
            // Arrange
            var productDto = new ProductDTO();
            _serviceMock.Setup(s => s.InsertAsync(It.IsAny<ProductDTO>())).ThrowsAsync(new Exception("Error message"));

            // Act
            var result = await _controller.Insert(productDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}
