using DsDelivery.WebApi.Controllers;
using DsDeliveryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DsDeliveryApi.Tests.ApiControllersTests
{
    public class OrderControllerTests
    {
        //[Fact]
        //public async Task FindAll_ReturnsOkResultWithListOfOrders()
        //{
        //    // Arrange
        //    var orders = new List<CreateOrderDTO>
        //    {
        //        new CreateOrderDTO { Address = "Address 1"},
        //        new CreateOrderDTO { Address = "Address 2"}
        //    };

        //    var serviceMock = new Mock<IOrderService>();
        //    serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(orders);

        //    var controller = new OrderController(serviceMock.Object);

        //    // Act
        //    var result = await controller.FindAll();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    var model = Assert.IsAssignableFrom<List<OrderDTO>>(okResult.Value);
        //    Assert.Equal(2, model.Count);
        //}

        //[Fact]
        //public async Task Insert_ReturnsCreatedResultWithOrder()
        //{
        //    // Arrange
        //    var dto = new OrderDTO { Address = "Address 1" };

        //    var serviceMock = new Mock<IOrderService>();
        //    serviceMock.Setup(s => s.InsertAsync(dto)).ReturnsAsync(dto);

        //    var controller = new OrderController(serviceMock.Object);

        //    // Act
        //    var result = await controller.Insert(dto);

        //    // Assert
        //    var createdResult = Assert.IsType<CreatedResult>(result.Result);
        //    var model = Assert.IsType<OrderDTO>(createdResult.Value);
        //    Assert.Equal(dto.Id, model.Id);
        //}

        //[Fact]
        //public async Task SetDelivered_ReturnsOkResultWithOrder()
        //{
        //    // Arrange
        //    var orderId = 1;
        //    var dto = new OrderDTO { Id = orderId, Address = "Address 1" };

        //    var serviceMock = new Mock<IOrderService>();
        //    serviceMock.Setup(s => s.SetDeliveredAsync(orderId)).ReturnsAsync(dto);

        //    var controller = new OrderController(serviceMock.Object);

        //    // Act
        //    var result = await controller.SetDelivered(orderId);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    var model = Assert.IsType<OrderDTO>(okResult.Value);
        //    Assert.Equal(orderId, model.Id);
        //}
    }
}
