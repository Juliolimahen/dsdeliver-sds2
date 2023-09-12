using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.FakeData.OrderData;
using DsDelivery.Manager.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace DsDelivery.WebApi.Controllers.Tests
{
    public class OrderControllerTest
    {
        private readonly IOrderService manager;
        private readonly ILogger<OrderController> logger;
        private readonly OrderController controller;
        private readonly OrderDTO orderDTO;
        private readonly List<OrderDTO> listaOrderDTO;
        private readonly CreateOrderDTO createOrderDTO;

        public OrderControllerTest()
        {
            manager = Substitute.For<IOrderService>();
            logger = Substitute.For<ILogger<OrderController>>();
            controller = new OrderController(manager, logger);
            orderDTO = new OrderFakerDtoRefactor().Generate();
            listaOrderDTO = new OrderFakerDtoRefactor().Generate(10);
            createOrderDTO = new CreateOrderFakerDto().Generate();
        }

        [Fact]
        public async Task GetAllOrders_Ok()
        {
            var controle = new List<OrderDTO>();
            listaOrderDTO.ForEach(p => controle.Add(p.CloneTipado()));

            manager.GetAllAsync().Returns(listaOrderDTO);

            var resultado = (ObjectResult)await controller.GetAll();

            await manager.Received().GetAllAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetAllOrders_NotFound()
        {
            manager.GetAllAsync().Returns(new List<OrderDTO>());

            var resultado = (StatusCodeResult)await controller.GetAll();

            await manager.Received().GetAllAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetOrder_GetById_Ok()
        {
            manager.GetByIdAsync(Arg.Any<int>()).Returns(orderDTO.CloneTipado());

            var resultado = (ObjectResult)await controller.GetOrderById(orderDTO.Id);

            await manager.Received().GetByIdAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(orderDTO);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetByIdOrder_NotFound()
        {
            manager.GetByIdAsync(Arg.Any<int>()).Returns(new OrderDTO());

            var resultado = (StatusCodeResult)await controller.GetOrderById(1);

            await manager.Received().GetByIdAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task PostOrder_Created()
        {
            manager.InsertAsync(Arg.Any<CreateOrderDTO>()).Returns(orderDTO.CloneTipado());

            var resultado = (ObjectResult)await controller.CreateOrder(createOrderDTO);

            await manager.Received().InsertAsync(Arg.Any<CreateOrderDTO>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(orderDTO);
        }

        [Fact]
        public async Task SetDelivered_Ok()
        {
            manager.SetDeliveredAsync(orderDTO.Id).Returns(orderDTO.CloneTipado());

            var resultado = await controller.SetDelivered(orderDTO.Id);

            await manager.Received().SetDeliveredAsync(orderDTO.Id);
            resultado.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task SetDelivered_NotFound()
        {
            manager.SetDeliveredAsync(orderDTO.Id).ReturnsNull();

            var resultado = await controller.SetDelivered(orderDTO.Id);

            resultado.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
