using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.FakeData.ProductData;
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
    public class ProductControllerTest
    {
        private readonly IProductService manager;
        private readonly ILogger<ProductController> logger;
        private readonly ProductController controller;
        private readonly ProductDTO productDTO;
        private readonly List<ProductDTO> listaProductDTO;
        private readonly CreateProductDTO createProductDTO;

        public ProductControllerTest()
        {
            manager = Substitute.For<IProductService>();
            logger = Substitute.For<ILogger<ProductController>>();
            controller = new ProductController(manager, logger);

            productDTO = new ProductFakerDtoRefactor().Generate();
            listaProductDTO = new ProductFakerDtoRefactor().Generate(10);
            createProductDTO = new CreateProductDtoFaker().Generate();
        }

        [Fact]
        public async Task GetAllProducts_Ok()
        {
            var controle = new List<ProductDTO>();
            listaProductDTO.ForEach(p => controle.Add(p.CloneTipado()));

            manager.GetAllAsync().Returns(listaProductDTO);

            var resultado = (ObjectResult)await controller.GetAll();

            await manager.Received().GetAllAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controle);
        }

        [Fact]
        public async Task GetAllProducts_NotFound()
        {
            manager.GetAllAsync().Returns(new List<ProductDTO>());

            var resultado = (StatusCodeResult)await controller.GetAll();

            await manager.Received().GetAllAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetProduct_GetById_Ok()
        {
            manager.GetByIdAsync(Arg.Any<int>()).Returns(productDTO.CloneTipado());

            var resultado = (ObjectResult)await controller.GetById(productDTO.Id);

            await manager.Received().GetByIdAsync(Arg.Any<int>());
            resultado.Value.Should().BeEquivalentTo(productDTO);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetByIdProduct_NotFound()
        {
            manager.GetByIdAsync(Arg.Any<int>()).Returns(new ProductDTO());

            var resultado = (StatusCodeResult)await controller.GetById(1);

            await manager.Received().GetByIdAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task PostProduct_Created()
        {
            manager.InsertAsync(Arg.Any<CreateProductDTO>()).Returns(productDTO.CloneTipado());

            var resultado = (ObjectResult)await controller.Insert(createProductDTO);

            await manager.Received().InsertAsync(Arg.Any<CreateProductDTO>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(productDTO);
        }

        [Fact]
        public async Task PutProduct_Ok()
        {
            manager.UpdateAsync(Arg.Any<UpdateProductDTO>()).Returns(productDTO.CloneTipado());

            var resultado = (ObjectResult)await controller.Update(productDTO.Id, new UpdateProductDTO());

            await manager.Received().UpdateAsync(Arg.Any<UpdateProductDTO>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(productDTO);
        }

        [Fact]
        public async Task PutProduct_NotFound()
        {
            manager.UpdateAsync(Arg.Any<UpdateProductDTO>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Update(productDTO.Id, new UpdateProductDTO());

            await manager.Received().UpdateAsync(Arg.Any<UpdateProductDTO>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task DeleteProduct_NoContent()
        {
            manager.DeleteAsync(Arg.Any<int>()).Returns(productDTO);

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await manager.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task NotFound_NotFound()
        {
            manager.DeleteAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await controller.Delete(1);

            await manager.Received().DeleteAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
