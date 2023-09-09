using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Data.Repositories.Interfaces;
using DsDelivery.FakeData;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Manager.Mapping;
using DsDelivery.Manager.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DsDelivery.Manager.Tests
{
    public class ProductServiceTest
    {
        private readonly IProductRepository repository;
        private readonly ILogger<ProductService> logger;
        private readonly IMapper mapper;
        private readonly IProductService manager;
        private readonly Product product;
        private readonly CreateProductDTO createProductDTO;
        private readonly UpdateProductDTO updateProductDTO;
        private readonly ProductFakerDto productFaker;
        private readonly CreateProductDtoFaker createProductDtoFaker;
        private readonly ProductUpdateDtoFaker updateProductFaker;

        public ProductServiceTest()
        {
            repository = Substitute.For<IProductRepository>();
            logger = Substitute.For<ILogger<ProductService>>();
            mapper = new MapperConfiguration(p => p.AddProfile<MappingProfile>()).CreateMapper();
            manager = new ProductService(repository, mapper);
            productFaker = new ProductFakerDto();
            createProductDtoFaker = new CreateProductDtoFaker();
            updateProductFaker = new ProductUpdateDtoFaker();
            product = productFaker.Generate();
            createProductDTO = createProductDtoFaker.Generate();
            updateProductDTO = updateProductFaker.Generate();
        }

        [Fact]
        public async Task GetProductsAsync_Sucesso()
        {
            var listaProducts = productFaker.Generate(10);
            repository.FindAllByOrderByNameAscAsync().Returns(listaProducts);
            var controle = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(listaProducts);
            var retorno = await manager.GetAllAsync();
            await repository.Received().FindAllByOrderByNameAscAsync();
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetProductsAsync_Vazio()
        {
            repository.FindAllByOrderByNameAscAsync().Returns((List<Product>)null);
            var retorno = await manager.GetAllAsync();
            retorno.Should().NotBeNull();
            retorno.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateProductAsync_Sucesso()
        {
            repository.UpdateAsync(Arg.Any<Product>()).Returns(product);
            var controle = mapper.Map<ProductDTO>(product);
            var retorno = await manager.UpdateAsync(updateProductDTO);
            await repository.Received().UpdateAsync(Arg.Any<Product>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task UpdateProductAsync_NaoEncontrado()
        {
            repository.UpdateAsync(Arg.Any<Product>()).ReturnsNull();
            var retorno = await manager.UpdateAsync(updateProductDTO);
            await repository.Received().UpdateAsync(Arg.Any<Product>());
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task GetProductAsync_Sucesso()
        {
            repository.GetByIdAsync(Arg.Any<int>()).Returns(product);
            var controle = mapper.Map<ProductDTO>(product);
            var retorno = await manager.GetByIdAsync(product.Id);
            await repository.Received().GetByIdAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetProductAsync_NaoEncontrado()
        {
            repository.GetByIdAsync(Arg.Any<int>()).Returns(new Product());
            var controle = mapper.Map<ProductDTO>(new Product());
            var retorno = await manager.GetByIdAsync(1);
            await repository.Received().GetByIdAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task InsertProductAsync_Sucesso()
        {
            repository.AddAsync(Arg.Any<Product>()).Returns(product);
            var controle = mapper.Map<ProductDTO>(product);
            var retorno = await manager.InsertAsync(createProductDTO);
            await repository.Received().AddAsync(Arg.Any<Product>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task DeleteProductAsync_Sucesso()
        {
            repository.RemoveAsync(Arg.Any<int>()).Returns(product);
            var controle = mapper.Map<ProductDTO>(product);
            var retorno = await manager.DeleteAsync(product.Id);
            await repository.Received().RemoveAsync(Arg.Any<int>());
            retorno.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task DeleteProductAsync_NaoEncontrado()
        {
            repository.RemoveAsync(Arg.Any<int>()).ReturnsNull();
            var retorno = await manager.DeleteAsync(1);
            await repository.Received().RemoveAsync(Arg.Any<int>());
            retorno.Should().BeNull();
        }
    }
}