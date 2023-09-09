using AutoMapper;
using NSubstitute;
using FluentAssertions;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Data.Repositories.Interfaces;
using DsDelivery.FakeData.OrderData;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Manager.Mapping;
using DsDelivery.Manager.Services;
using Microsoft.Extensions.Logging;
using NSubstitute.ReturnsExtensions;


namespace DsDelivery.Manager.Tests
{
    public class OrderServiceTest
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly ILogger<OrderService> logger;
        private readonly IMapper mapper;
        private readonly IOrderService manager;
        private readonly Order order;
        private readonly CreateOrderDTO createOrderDTO;
        private readonly UpdateOrderDTO updateOrderDTO;
        private readonly OrderFakerDto orderFaker;
        private readonly CreateOrderFakerDto createOrderDtoFaker;
        private readonly UpdateOrderFakerDto updateOrderFaker;

        public OrderServiceTest()
        {
            orderRepository = Substitute.For<IOrderRepository>();
            productRepository = Substitute.For<IProductRepository>();
            logger = Substitute.For<ILogger<OrderService>>();
            mapper = new MapperConfiguration(p => p.AddProfile<MappingProfile>()).CreateMapper();
            manager = new OrderService(mapper, orderRepository, productRepository);
            orderFaker = new OrderFakerDto();
            createOrderDtoFaker = new CreateOrderFakerDto();
            updateOrderFaker = new UpdateOrderFakerDto();
            order = orderFaker.Generate();
            createOrderDTO = createOrderDtoFaker.Generate();
            updateOrderDTO = updateOrderFaker.Generate();
        }

        [Fact]
        public async Task GetOrderAsync_Sucesso()
        {
            orderRepository.GetByIdAsync(Arg.Any<int>()).Returns(order);
            var controle = mapper.Map<OrderDTO>(order);
            var retorno = await manager.GetByIdAsync(order.Id);
            await orderRepository.Received().GetByIdAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetProductsAsync_Sucesso()
        {
            orderRepository.FindOrdersWithProducts().Returns(new List<Order>());
            var retorno = await manager.GetAllAsync();
            await orderRepository.Received().FindOrdersWithProducts();
            retorno.Should().BeEquivalentTo(new List<Order>());
        }

        [Fact]
        public async Task SetDeliveredAsync_Sucesso()
        {
            int orderId = 1;
            var order = orderFaker.Generate();
            order.Id = orderId;
            order.Status = OrderStatus.PENDING;
            orderRepository.GetByIdAsync(orderId).Returns(order);
            orderRepository.UpdateAsync(Arg.Any<Order>()).Returns(callInfo => callInfo.Arg<Order>());
            var result = await manager.SetDeliveredAsync(orderId);
            await orderRepository.Received().GetByIdAsync(orderId);
            order.Status.Should().Be(OrderStatus.DELIVERED);
            await orderRepository.Received().UpdateAsync(order);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mapper.Map<OrderDTO>(order));
        }

        [Fact]
        public async Task SetDeliveredAsync_NaoEncontrado()
        {
            int orderId = 1;
            orderRepository.GetByIdAsync(orderId).ReturnsNull();
            var result = await manager.SetDeliveredAsync(orderId);
            await orderRepository.Received().GetByIdAsync(orderId);
            result.Should().BeNull();
        }

        //[Fact]
        //public async Task SetDeliveredAsync_Sucesso()
        //{
        //    int orderId = 1; 
        //    var order = new Order { Id = orderId, Status = OrderStatus.PENDING };
        //    orderRepository.GetByIdAsync(orderId).Returns(order);
        //    orderRepository.UpdateAsync(Arg.Any<Order>()).Returns(callInfo => callInfo.Arg<Order>());
        //    var result = await manager.SetDeliveredAsync(orderId);
        //    await orderRepository.Received().GetByIdAsync(orderId);
        //    order.Status.Should().Be(OrderStatus.DELIVERED);
        //    await orderRepository.Received().UpdateAsync(order);
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(mapper.Map<OrderDTO>(order));
        //}

        //[Fact]
        //public async Task SetDeliveredAsync_NaoEncontrado()
        //{
        //    int orderId = 1;
        //    orderRepository.GetByIdAsync(orderId).ReturnsNull();
        //    var result = await manager.SetDeliveredAsync(orderId);
        //    await orderRepository.Received().GetByIdAsync(orderId);
        //    result.Should().BeNull();
        //}

        [Fact]
        public async Task GetOrdersAsync_Vazio()
        {
            orderRepository.GetAllAsync().Returns((List<Order>)null);
            var retorno = await manager.GetAllAsync();
            retorno.Should().NotBeNull();
            retorno.Should().BeEmpty();
        }

        [Fact]
        public async Task GetOrderAsync_NaoEncontrado()
        {
            orderRepository.GetByIdAsync(Arg.Any<int>()).Returns(new Order());
            var controle = mapper.Map<OrderDTO>(new Order());
            var retorno = await manager.GetByIdAsync(1);
            await orderRepository.Received().GetByIdAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task InsertOrderAsync_Sucesso()
        {
            orderRepository.AddAsync(Arg.Any<Order>()).Returns(order);
            var retorno = await manager.InsertAsync(createOrderDTO);
            await orderRepository.Received().AddAsync(Arg.Any<Order>());
            retorno.Should().NotBeNull();
            retorno.Should().BeEquivalentTo(mapper.Map<OrderDTO>(order));
        }

        [Fact]
        public async Task InsertOrderAsync_Falha()
        {
            orderRepository.AddAsync(Arg.Any<Order>()).ReturnsNull();
            var retorno = await manager.InsertAsync(createOrderDTO);
            await orderRepository.Received().AddAsync(Arg.Any<Order>());
            retorno.Should().BeNull();
        }
    }
}
