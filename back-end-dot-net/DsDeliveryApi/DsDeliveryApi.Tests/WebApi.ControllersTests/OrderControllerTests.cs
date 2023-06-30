using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DsDelivery.WebApi.Controllers.Tests
{
    public class OrderControllerTests
    {
        private readonly OrderController _controller;
        private readonly Mock<IOrderService> _serviceMock;
        private readonly Mock<ILogger<OrderController>> _loggerMock;
        public OrderControllerTests()
        {
            _serviceMock = new Mock<IOrderService>();
            _loggerMock = new Mock<ILogger<OrderController>>();
            _controller = new OrderController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenServiceReturnsList_ReturnsOkResultWithList()
        {
            // Arrange


            var expectedList = new List<OrderDTO>
            {
                new OrderDTO
                {
                    Id=1,
                    Address = "Avenida Paulista, 1500",
                    Latitude = -23.56168,
                    Longitude = -46.656139,
                    Moment = DateTime.Parse("2021-01-01T10:00:00"),
                    Status = OrderStatus.PENDING,
                    Total = 101.9,
                    Products = new List<ProductDTO>
                    {
                        new ProductDTO
                        {
                            Id = 1,
                            Name = "Pizza Bacon",
                            Price = 49.9,
                            Description = "Pizza de bacon com mussarela, orégano, molho especial e tempero da casa.",
                            ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/pizza_bacon.jpg"
                        },
                        new ProductDTO
                        {
                            Id = 4,
                            Name = "Risoto de Carne",
                            Price = 52,
                            Description = "Risoto de carne com especiarias e um delicioso molho de acompanhamento.",
                            ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_carne.jpg"
                        }
                    }
                }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedList);

            // Act
            IActionResult result = await _controller.GetAll();

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<OrderDTO> actualList = Assert.IsType<List<OrderDTO>>(okResult.Value);
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public async Task GetAll_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllAsync()).ThrowsAsync(new Exception("Some error"));

            // Act
            IActionResult result = await _controller.GetAll();

            // Assert
            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetOrderById_WhenValidId_ReturnsOkResultWithOrder()
        {
            // Arrange
            int orderId = 1;
            OrderDTO expectedOrder = new OrderDTO
            {
                Id = 1,
                Address = "Avenida Paulista, 1500",
                Latitude = -23.56168,
                Longitude = -46.656139,
                Moment = DateTime.Parse("2021-01-01T10:00:00"),
                Status = OrderStatus.PENDING,
                Total = 101.9,
                Products = new List<ProductDTO>
                    {
                        new ProductDTO
                        {
                            Id = 1,
                            Name = "Pizza Bacon",
                            Price = 49.9,
                            Description = "Pizza de bacon com mussarela, orégano, molho especial e tempero da casa.",
                            ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/pizza_bacon.jpg"
                        },
                        new ProductDTO
                        {
                            Id = 4,
                            Name = "Risoto de Carne",
                            Price = 52,
                            Description = "Risoto de carne com especiarias e um delicioso molho de acompanhamento.",
                            ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_carne.jpg"
                        }
                    }
            };

            _serviceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync(expectedOrder);

            // Act
            IActionResult result = await _controller.GetOrderById(orderId);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            OrderDTO actualOrder = Assert.IsType<OrderDTO>(okResult.Value);
            Assert.Equal(expectedOrder, actualOrder);
        }

        [Fact]
        public async Task GetOrderById_WhenInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int orderId = 1;
            _serviceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync((OrderDTO)null);

            // Act
            IActionResult result = await _controller.GetOrderById(orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetOrderById_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            int orderId = 1;
            _serviceMock.Setup(s => s.GetByIdAsync(orderId)).ThrowsAsync(new Exception("Some error"));

            // Act
            IActionResult result = await _controller.GetOrderById(orderId);

            // Assert
            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_WhenValidDto_ReturnsCreatedAtActionResultWithOrder()
        {
            // Arrange
            CreateOrderDTO orderDTO = new CreateOrderDTO
            {
                Address = "Avenida Paulista, 1500",
                Latitude = -23.56168,
                Longitude = -46.656139,
                Products = new List<ReferenciaProducts>
                    {
                        new ReferenciaProducts
                        {
                            Id = 1
                        },
                        new ReferenciaProducts
                        {
                            Id = 4
                        }
                    }
            };

            OrderDTO expectedOrder = new OrderDTO { Id = 1 };
            _serviceMock.Setup(s => s.InsertAsync(orderDTO)).ReturnsAsync(expectedOrder);

            // Act
            IActionResult result = await _controller.CreateOrder(orderDTO);

            // Assert
            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(OrderController.GetOrderById), createdAtActionResult.ActionName);
            Assert.Equal(expectedOrder.Id, createdAtActionResult.RouteValues["id"]);
            OrderDTO actualOrder = Assert.IsType<OrderDTO>(createdAtActionResult.Value);
            Assert.Equal(expectedOrder, actualOrder);
        }

        [Fact]
        public async Task CreateOrder_WhenInvalidDto_ReturnsBadRequestResult()
        {
            // Arrange

            CreateOrderDTO orderDTO = new CreateOrderDTO
            {
                Address = "Avenida Paulista, 1500",
                Latitude = -23.56168,
                Longitude = -46.656139,
                Products = new List<ReferenciaProducts>
                    {
                        new ReferenciaProducts
                        {
                            Id = 1
                        },
                        new ReferenciaProducts
                        {
                            Id = 4
                        }
                    }
            };

            _controller.ModelState.AddModelError("PropertyName", "Error message");

            // Act
            IActionResult result = await _controller.CreateOrder(orderDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateOrder_WhenServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange

            CreateOrderDTO orderDTO = new CreateOrderDTO
            {
                Address = "Avenida Paulista, 1500",
                Latitude = -23.56168,
                Longitude = -46.656139,
                Products = new List<ReferenciaProducts>
                    {
                        new ReferenciaProducts
                        {
                            Id = 1
                        },
                        new ReferenciaProducts
                        {
                            Id = 4
                        }
                    }
            };

            _serviceMock.Setup(s => s.InsertAsync(orderDTO)).ThrowsAsync(new Exception("Some error"));

            // Act
            IActionResult result = await _controller.CreateOrder(orderDTO);

            // Assert
            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SetDelivered_ReturnsOkResultWithOrder()
        {
            // Arrange
            var orderId = 1;
            var dto = new OrderDTO
            {
                Id = 1,
                Address = "Avenida Paulista, 1500",
                Latitude = -23.56168,
                Longitude = -46.656139,
                Moment = DateTime.Parse("2021-01-01T10:00:00"),
                Status = OrderStatus.PENDING,
                Total = 101.9,
                Products = new List<ProductDTO>
                {
                    new ProductDTO
                    {
                        Id = 1,
                        Name = "Pizza Bacon",
                        Price = 49.9,
                        Description = "Pizza de bacon com mussarela, orégano, molho especial e tempero da casa.",
                        ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/pizza_bacon.jpg"
                    },
                    new ProductDTO
                    {
                        Id = 4,
                        Name = "Risoto de Carne",
                        Price = 52,
                        Description = "Risoto de carne com especiarias e um delicioso molho de acompanhamento.",
                        ImageUri = "https://raw.githubusercontent.com/devsuperior/sds2/master/assets/risoto_carne.jpg"
                    }
                }
            };

            var serviceMock = new Mock<IOrderService>();
            serviceMock.Setup(s => s.SetDeliveredAsync(orderId)).ReturnsAsync(dto);

            var controller = new OrderController(serviceMock.Object, null);

            // Act
            var result = await controller.SetDelivered(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<OrderDTO>(okResult.Value);
            Assert.Equal(orderId, model.Id);
        }

    }
}
