using Bogus;
using DsDelivery.Core.Shared.Dto.Order;


namespace DsDelivery.FakeData.OrderData;

public class ReferenciaProductsFaker : Faker<ReferenciaProducts>
{
    public ReferenciaProductsFaker()
    {
        RuleFor(p => p.Id, f => f.Random.Number(1, 100));
    }
}
