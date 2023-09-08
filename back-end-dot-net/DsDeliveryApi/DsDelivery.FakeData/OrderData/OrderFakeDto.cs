﻿using Bogus;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;

public class OrderFakeDto : Faker<Order>
{
    public OrderFakeDto()
    {
        var id = new Faker().Random.Number(1, 999999);
        RuleFor(o => o.Id, f => id);
        RuleFor(o => o.Address, f => f.Address.FullAddress());
        RuleFor(o => o.Latitude, f => f.Address.Latitude());
        RuleFor(o => o.Longitude, f => f.Address.Longitude());
        RuleFor(o => o.Moment, f => f.Date.Past(1));
        RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>());

        // Generate a list of product IDs
        RuleFor(o => o.OrderProducts, (f, o) => GenerateOrderProducts(f, o));
    }

    private List<OrderProduct> GenerateOrderProducts(Faker f, Order order)
    {
        var productIds = new List<int>();

        // Generate a list of unique product IDs
        for (var i = 0; i < f.Random.Int(1, 5); i++)
        {
            var productId = f.Random.Number(1, 100); // Assuming product IDs range from 1 to 100
            if (!productIds.Contains(productId))
            {
                productIds.Add(productId);
            }
        }

        // Create OrderProduct objects from the generated product IDs
        var orderProducts = productIds.Select(productId => new OrderProduct
        {
            OrderId = order.Id,
            ProductId = productId,
        }).ToList();

        return orderProducts;
    }
}