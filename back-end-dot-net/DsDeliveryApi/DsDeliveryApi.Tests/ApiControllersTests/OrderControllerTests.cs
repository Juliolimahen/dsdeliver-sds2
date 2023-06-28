using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared;
using DsDelivery.Manager.Interfaces;
using DsDelivery.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DsDeliveryApi.Tests.ApiControllersTests
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task FindAll_ReturnsOkResultWithListOfOrders()
        {
            // Arrange
            var orders = new List<OrderDTO>
            {
                new OrderDTO
                {
                    Id=1,
                    Address = "Avenida Paulista, 1500",
                    Latitude = -23.56168,
                    Longitude = -46.656139,
                    Moment = DateTime.Parse("2021-01-01T10:00:00"),
                    Status = OrderStatus.PENDING.ToString(),
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

            var serviceMock = new Mock<IOrderService>();
            serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(orders);

            var controller = new OrderController(serviceMock.Object);

            // Act
            var result = await controller.FindAll();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var model = Assert.IsAssignableFrom<List<OrderDTO>>(okResult.Value);
            Assert.Equal(orders.Count, model.Count);
        }

        [Fact]
        public async Task Insert_ReturnsCreatedResultWithOrder()
        {
            // Arrange
            var dto = new OrderDTO
            {
                Id = 1,
                Address = "Avenida Paulista, 1500",
                Latitude = -23.56168,
                Longitude = -46.656139,
                Moment = DateTime.Parse("2021-01-01T10:00:00"),
                Status = "PENDING",
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
            serviceMock.Setup(s => s.InsertAsync(It.IsAny<OrderDTO>())).ReturnsAsync(dto);

            var controller = new OrderController(serviceMock.Object);

            // Act
            var result = await controller.Insert(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<OrderDTO>(createdResult.Value);
            Assert.Equal(dto.Id, model.Id);
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
                Status = OrderStatus.PENDING.ToString(),
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
