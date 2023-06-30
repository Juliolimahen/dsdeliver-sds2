using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Manager.Interfaces;
using DsDelivery.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DsDelivery.WebApi.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly OrderController _controller;
        private readonly Mock<IOrderService> _serviceMock;

        public OrderControllerTests()
        {
            _serviceMock = new Mock<IOrderService>();
            _controller = new OrderController(_serviceMock.Object);
        }

        [Fact]
        public async Task FindAll_ReturnsOkResultWithData()
        {
            var orderDtoList = new List<OrderDTO>
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
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(orderDtoList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<OrderDTO>>(okResult.Value);
            Assert.Equal(orderDtoList, model);
        }

        [Fact]
        public async Task CreateOrder_WithValidDto_ReturnsCreatedAtActionResult()
        {
            // Arrange

            var createDto = new CreateOrderDTO
            {
                Address = "Avenida Paulista, 1500",
                Latitude = -23.56168,
                Longitude = -46.656139,
                Products = new List<ReferenciaProducts>
                {
                    new ReferenciaProducts
                    {
                        Id = 1,
                    },
                    new ReferenciaProducts
                    {
                        Id = 2,
                    }
                }
            };

            var orderDto = new OrderDTO
            {
                Address = createDto.Address,
                Latitude = createDto.Latitude,
                Longitude = createDto.Longitude,
                Products = createDto.Products.Select(p => new ProductDTO { Id = p.Id }).ToList()
            };

            _serviceMock.Setup(s => s.InsertAsync(It.IsAny<CreateOrderDTO>())).ReturnsAsync(orderDto);

            // Act
            var result = await _controller.CreateOrder(createDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<OrderDTO>(createdAtActionResult.Value);
            Assert.Equal(orderDto, model);
        }

        [Fact]
        public async Task CreateOrder_WithInvalidDto_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateOrderDTO();
            _controller.ModelState.AddModelError("Key", "Error message");

            // Act
            var result = await _controller.CreateOrder(createDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetOrderById_WithValidId_ReturnsOkResultWithData()
        {
            // Arrange
            int orderId = 1;

            var orderDto = new OrderDTO
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

            _serviceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync(orderDto);

            // Act
            var result = await _controller.GetOrderById(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<OrderDTO>(okResult.Value);
            Assert.Equal(orderDto, model);
        }

        [Fact]
        public async Task GetOrderById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int orderId = 1;
            _serviceMock.Setup(s => s.GetByIdAsync(orderId)).ReturnsAsync((OrderDTO)null);

            // Act
            var result = await _controller.GetOrderById(orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
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

            var controller = new OrderController(serviceMock.Object);

            // Act
            var result = await controller.SetDelivered(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<OrderDTO>(okResult.Value);
            Assert.Equal(orderId, model.Id);
        }
    }
}
