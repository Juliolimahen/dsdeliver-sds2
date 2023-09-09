﻿using Bogus;
using DsDelivery.Core.Shared.Dto.Order;


namespace DsDelivery.FakeData.OrderData;

public class UpdateOrderFakerDto : Faker<UpdateOrderDTO>
{
    public UpdateOrderFakerDto()
    {
        RuleFor(o => o.Id, f => f.Random.Number(1, 100));
        RuleFor(o => o.Address, f => f.Address.FullAddress());
        RuleFor(o => o.Latitude, f => f.Address.Latitude());
        RuleFor(o => o.Longitude, f => f.Address.Longitude());
        RuleFor(o => o.Products, f => GenerateProducts(f));
    }
    private List<ReferenciaProducts> GenerateProducts(Faker f)
    {
        var productFaker = new ReferenciaProductsFaker();
        var products = productFaker.Generate(5);
        return products;
    }
}
