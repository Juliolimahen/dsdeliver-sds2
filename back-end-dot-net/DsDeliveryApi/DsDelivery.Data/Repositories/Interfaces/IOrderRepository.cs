using DsDelivery.Core.Domain;
using System.Linq.Expressions;

namespace DsDelivery.Data.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> FindOrdersWithProducts();
}
