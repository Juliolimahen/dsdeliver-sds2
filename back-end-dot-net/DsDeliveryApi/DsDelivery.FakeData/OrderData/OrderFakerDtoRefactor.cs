
using AutoMapper;
using Bogus;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.FakeData.ProductData;
using DsDelivery.Manager.Mapping;
using System.Data;

namespace DsDelivery.FakeData.OrderData;

public class OrderFakerDtoRefactor : Faker<OrderDTO>
{
    public OrderFakerDtoRefactor()
    {
        var id = new Faker().Random.Number(1, 999999);
        RuleFor(o => o.Id, f => id);
        RuleFor(o => o.Address, f => f.Address.FullAddress());
        RuleFor(o => o.Latitude, f => f.Address.Latitude());
        RuleFor(o => o.Longitude, f => f.Address.Longitude());
        RuleFor(o => o.Moment, f => f.Date.Past(1));
        RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>());
        RuleFor(o => o.Products, f => GenerateProducts(f));
    }

    private List<ProductDTO> GenerateProducts(Faker f)
    {
        var productFaker = new ProductFakerDto();
        var products = productFaker.Generate(5);
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        var productDTOs = mapper.Map<List<ProductDTO>>(products);
        return productDTOs;
    }
}
