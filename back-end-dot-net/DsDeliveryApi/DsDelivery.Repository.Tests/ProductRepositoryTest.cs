using DsDelivery.Core.Domain;
using DsDelivery.Data.Repositories;
using DsDelivery.Data.Repositories.Interfaces;
using DsDelivery.FakeData;
using DsDeliveryApi.Data.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;


namespace DsDelivery.Repository.Tests
{
    public class ProductRepositoryTest : IDisposable
    {
        private readonly IProductRepository repository;
        private readonly AppDbContext context;
        private readonly Product product;
        private readonly ProductFakerDto productFaker;

        public ProductRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new AppDbContext(optionsBuilder.Options);
            repository = new ProductRepository(context);
            productFaker = new ProductFakerDto();
            product = productFaker.Generate();
        }

        private async Task<List<Product>> InsereRegistros()
        {
            var products = productFaker.Generate(100);
            foreach (var cli in products)
            {
                cli.Id = 0;
                await context.Products.AddAsync(cli);
            }
            await context.SaveChangesAsync();
            return products;
        }

        [Fact]
        public async Task GetProductsAsync_ComRetorno()
        {
            var registros = await InsereRegistros();
            var retorno = await repository.GetAllAsync();
            retorno.Should().HaveCount(registros.Count);
        }

        [Fact]
        public async Task GetProductsAsync_Vazio()
        {
            var retorno = await repository.GetAllAsync();
            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetProductAsync_Encontrado()
        {
            var registros = await InsereRegistros();
            var retorno = await repository.GetByIdAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task FindAllByOrderByNameAscAsync_ComRetorno()
        {
            var registros = await InsereRegistros();
            var retorno = await repository.FindAllByOrderByNameAscAsync();
            retorno.Should().HaveCount(registros.Count);
            var sortedProducts = registros.OrderBy(p => p.Name).ToList();
            for (int i = 0; i < registros.Count; i++)
            {
                retorno[i].Id.Should().Be(sortedProducts[i].Id);
                retorno[i].Name.Should().Be(sortedProducts[i].Name);
            }
        }

        [Fact]
        public async Task FindAllByOrderByNameAscAsync_Vazio()
        {
            var retorno = await repository.FindAllByOrderByNameAscAsync();
            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetProductAsync_NaoEncontrado()
        {
            var retorno = await repository.GetByIdAsync(1);
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InsertProductAsync_Sucesso()
        {
            var retorno = await repository.AddAsync(product);
            retorno.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task UpdateProductAsync_Sucesso()
        {
            var registros = await InsereRegistros();
            var productParaAtualizar = registros.First();
            productParaAtualizar.Name = "Novo Nome";
            productParaAtualizar.Price = 99.99;
            var retorno = await repository.UpdateAsync(productParaAtualizar);
            retorno.Should().NotBeNull();
            retorno.Id.Should().Be(productParaAtualizar.Id);
        }

        [Fact]
        public async Task UpdateProductAsync_NaoEncontrado()
        {
            var idNaoExistente = 9999;
            var produtoNaoExistente = productFaker.Generate();
            produtoNaoExistente.Id = idNaoExistente;
            Func<Task> action = async () => await repository.UpdateAsync(produtoNaoExistente);
            var exception = await Assert.ThrowsAsync<Exception>(action);
            exception.Message.Should().Be($"Entidade com o ID {idNaoExistente} não foi encontrada.");
        }

        [Fact]
        public async Task DeleteProductAsync_Sucesso()
        {
            var registros = await InsereRegistros();
            var retorno = await repository.RemoveAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task DeleteProductAsync_NaoEncontrado()
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
