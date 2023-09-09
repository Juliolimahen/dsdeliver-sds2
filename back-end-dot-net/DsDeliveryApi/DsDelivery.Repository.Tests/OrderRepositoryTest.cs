using DsDelivery.Core.Domain;
using DsDelivery.Data.Repositories.Interfaces;
using DsDelivery.Data.Repositories;
using DsDeliveryApi.Data.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DsDelivery.FakeData.OrderData;

namespace DsDelivery.Repository.Tests
{
    public class OrderRepositoryTest : IDisposable
    {
        private readonly IOrderRepository repository;
        private readonly AppDbContext context;
        private readonly Order order;
        private readonly OrderFakerDto orderFaker;

        public OrderRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            context = new AppDbContext(optionsBuilder.Options);
            repository = new OrderRepository(context);

            orderFaker = new OrderFakerDto();
            order = orderFaker.Generate();
        }

        private async Task<List<Order>> InsereRegistros()
        {
            var orders = orderFaker.Generate(100);
            foreach (var item in orders)
            {
                item.Id = 0;
                await context.Orders.AddAsync(item);
            }
            await context.SaveChangesAsync();
            return orders;
        }

        [Fact]
        public async Task FindOrdersWithProductsAsync_Deve_Retornar_Ordens_Com_Produtos()
        {
            // Arrange
            var registros = await InsereRegistros();

            // Act
            var ordersWithProducts = await repository.FindOrdersWithProducts();

            // Assert
            ordersWithProducts.Should().NotBeNull();
            ordersWithProducts.Should().HaveCount(registros.Count);
            foreach (var order in ordersWithProducts)
            {
                order.OrderProducts.Should().NotBeNull();
                order.OrderProducts.Should().NotBeEmpty();
            }
        }

        [Fact]
        public async Task GetOrdersAsync_ComRetorno()
        {
            var registros = await InsereRegistros();
            var retorno = await repository.GetAllAsync();

            retorno.Should().HaveCount(registros.Count);
        }

        [Fact]
        public async Task GetOrdersAsync_Vazio()
        {
            var retorno = await repository.GetAllAsync();
            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetOrderAsync_Encontrado()
        {
            var registros = await InsereRegistros();
            var retorno = await repository.GetByIdAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task GetOrderAsync_NaoEncontrado()
        {
            var retorno = await repository.GetByIdAsync(1);
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InsertOrderAsync_Sucesso()
        {
            var retorno = await repository.AddAsync(order);
            retorno.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task UpdateOrderAsync_Sucesso()
        {
            var registros = await InsereRegistros();
            var orderParaAtualizar = registros.First();
            orderParaAtualizar.Status = OrderStatus.DELIVERED;
            var retorno = await repository.UpdateAsync(orderParaAtualizar);
            retorno.Should().NotBeNull();
            retorno.Id.Should().Be(orderParaAtualizar.Id);
        }

        [Fact]
        public async Task UpdateOrderAsync_NaoEncontrado()
        {
            var idNaoExistente = 9999;

            var orderNaoExistente = orderFaker.Generate();
            orderNaoExistente.Id = idNaoExistente;

            Func<Task> action = async () => await repository.UpdateAsync(orderNaoExistente);

            var exception = await Assert.ThrowsAsync<Exception>(action);
            exception.Message.Should().Be($"Entidade com o ID {idNaoExistente} não foi encontrada.");
        }


        [Fact]
        public async Task DeleteOrderAsync_Sucesso()
        {
            var registros = await InsereRegistros();
            var retorno = await repository.RemoveAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task DeleteOrderAsync_NaoEncontrado()
        {
            Func<Task> retorno = async () => await repository.RemoveAsync(1);

            await retorno.Should().ThrowAsync<Exception>()
                .WithMessage("Entidade com o ID 1 não foi encontrada.");
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
