using Bogus;
using DsDelivery.Core.Domain;

namespace DsDelivery.FakeData.OrderData;

public class OrderFakerDto : Faker<Order>
{
    public OrderFakerDto()
    {
        var id = new Faker().Random.Number(1, 999999);
        RuleFor(o => o.Id, f => id);
        RuleFor(o => o.Address, f => f.Address.FullAddress());
        RuleFor(o => o.Latitude, f => f.Address.Latitude());
        RuleFor(o => o.Longitude, f => f.Address.Longitude());
        RuleFor(o => o.Moment, f => f.Date.Past(1));
        RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>());
        RuleFor(o => o.OrderProducts, (f, o) => GenerateOrderProducts(f, o));
    }

    private List<OrderProduct> GenerateOrderProducts(Faker f, Order order)
    {
        var productIds = new List<int>();

        for (var i = 0; i < f.Random.Int(1, 5); i++)
        {
            var productId = f.Random.Number(1, 100);
            if (!productIds.Contains(productId))
            {
                productIds.Add(productId);
            }
        }

        var orderProducts = productIds.Select(productId => new OrderProduct
        {
            OrderId = order.Id,
            ProductId = productId,
        }).ToList();

        return orderProducts;
    }
}
