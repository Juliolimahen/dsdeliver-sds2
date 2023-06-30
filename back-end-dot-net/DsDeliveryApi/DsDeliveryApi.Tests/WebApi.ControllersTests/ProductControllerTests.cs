using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DsDelivery.WebApi.Controllers.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _serviceMock;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResultWithData()
        {
            // Arrange
            var products = new List<ProductDTO>
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "Risoto Funghi",
                    Price = 59.95,
                    Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                    ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "Risoto Funghfkfg",
                    Price = 55.95,
                    Description = "Risoto Funghi feito com  hjffkklj ingredientes finos e o toque especial do chef.",
                    ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
                }
            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<ProductDTO>>(okResult.Value);
            Assert.Equal(products, model);
        }

        [Fact]
        public async Task GetById_WhenValidProductId_ReturnsOkResultWithProduct()
        {
            // Arrange
            int productId = 1;
            ProductDTO expectedProduct = new ProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            IActionResult result = await _controller.GetById(productId);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            ProductDTO actualProduct = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(expectedProduct, actualProduct);
        }

        [Fact]
        public async Task GetById_WhenInvalidProductId_ReturnsNotFoundResult()
        {
            // Arrange
            int productId = 1;
            _serviceMock.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync((ProductDTO)null);

            // Act
            IActionResult result = await _controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int productId = 1;
            _serviceMock.Setup(s => s.GetByIdAsync(productId)).ThrowsAsync(new Exception("Some error"));

            // Act
            IActionResult result = await _controller.GetById(productId);

            // Assert
            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Insert_WhenValidProductDTO_ReturnsCreatedAtActionResultWithInsertedProduct()
        {
            // Arrange
            CreateProductDTO productDTO = new CreateProductDTO
            {
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            ProductDTO expectedInsertedProduct = new ProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.InsertAsync(productDTO)).ReturnsAsync(expectedInsertedProduct);

            // Act
            IActionResult result = await _controller.Insert(productDTO);

            // Assert
            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(ProductController.GetById), createdAtActionResult.ActionName);
            Assert.Equal(expectedInsertedProduct.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(expectedInsertedProduct, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Insert_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            CreateProductDTO productDTO = new CreateProductDTO
            {

                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.InsertAsync(productDTO)).ThrowsAsync(new Exception("Some error"));

            // Act
            IActionResult result = await _controller.Insert(productDTO);

            // Assert
            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Update_WhenValidIdAndProductDTO_ReturnsOkResultWithUpdatedProduct()
        {
            // Arrange
            int productId = 1;
            UpdateProductDTO productDTO = new UpdateProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            ProductDTO expectedProduct = new ProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.UpdateAsync(productDTO)).ReturnsAsync(expectedProduct);

            // Act
            IActionResult result = await _controller.Update(productId, productDTO);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            ProductDTO actualProduct = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(expectedProduct, actualProduct);
        }

        [Fact]
        public async Task Update_WhenInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int productId = 1;
            UpdateProductDTO productDTO = new UpdateProductDTO
            {
                Id = 2,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.UpdateAsync(productDTO)).ReturnsAsync((ProductDTO)null);

            // Act
            IActionResult result = await _controller.Update(productId, productDTO);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int productId = 1;
            UpdateProductDTO productDTO = new UpdateProductDTO
            {
                Id = 1,
                Name = "Risoto Funghi",
                Price = 59.95,
                Description = "Risoto Funghi feito com ingredientes finos e o toque especial do chef.",
                ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_funghi.jpg"
            };

            _serviceMock.Setup(s => s.UpdateAsync(productDTO)).ThrowsAsync(new Exception("Some error"));

            // Act
            IActionResult result = await _controller.Update(productId, productDTO);

            // Assert
            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Delete_WhenValidId_ReturnsNoContentResult()
        {
            // Arrange
            int productId = 1;
            _serviceMock.Setup(s => s.DeleteAsync(productId)).ReturnsAsync(new ProductDTO { Id = productId });

            // Act
            IActionResult result = await _controller.Delete(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WhenInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int productId = 1;
            _serviceMock.Setup(s => s.DeleteAsync(productId)).ReturnsAsync((ProductDTO)null);

            // Act
            IActionResult result = await _controller.Delete(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int productId = 1;
            _serviceMock.Setup(s => s.DeleteAsync(productId)).ThrowsAsync(new Exception("Some error"));

            // Act
            IActionResult result = await _controller.Delete(productId);

            // Assert
            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}
