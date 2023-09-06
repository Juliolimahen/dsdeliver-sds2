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
        private readonly IProductRepository _repository;
        private readonly AppDbContext _context;
        private readonly Product _product;
        private ProductFakerDto _productFaker;

        public ProductRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("Db_Test");

            _context = new AppDbContext(optionsBuilder.Options);
            _repository = new ProductRepository(_context);

            _productFaker = new ProductFakerDto();
            _product = _productFaker.Generate();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        private async Task<List<Product>> InsereRegistros()
        {
            var clientes = _productFaker.Generate(100);
            foreach (var item in clientes)
            {
                item.Id = 0;
                await _context.Products.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return clientes;
        }

        [Fact]
        public async Task Get_Products_Async_Com_Retorno()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.GetAllAsync();

            retorno.Should().HaveCount(registros.Count);
        }

        [Fact]
        public async Task Get_Products_Async_Vazio()
        {
            var retorno = await _repository.GetAllAsync();
            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetProductAsyncEncontrado()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.GetByIdAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task GetProductAsyncNaoEncontrado()
        {
            var retorno = await _repository.GetByIdAsync(1);
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InsertProductAsyncSucesso()
        {
            var retorno = await _repository.AddAsync(_product);
            retorno.Should().BeEquivalentTo(_product);
        }

        [Fact]
        public async Task UpdateProductAsyncSucesso()
        {
            var registros = await InsereRegistros();
            var productAlterado = _productFaker.Generate();
            productAlterado.Id = registros.First().Id;
            var retorno = await _repository.UpdateAsync(productAlterado);
            retorno.Should().BeEquivalentTo(productAlterado);
        }

        [Fact]
        public async Task Update_Product_Async_Nao_Encontrado()
        {
            var retorno = await _repository.UpdateAsync(_product);
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task Delete_Product_Async_Sucesso()
        {
            var registros = await InsereRegistros();
            var retorno = await _repository.RemoveAsync(registros.First().Id);
            retorno.Should().BeEquivalentTo(registros.First());
        }

        [Fact]
        public async Task Delete_Product_Async_Nao_Encontrado()
        {
            var retorno = await _repository.RemoveAsync(1);
            retorno.Should().BeNull();
        }

    }
}
