using DsDelivery.Core.Domain;

namespace DsDelivery.Data.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> FindOrdersWithProducts();
}
