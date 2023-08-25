using DsDelivery.Core.Domain;
using System.Linq.Expressions;

namespace DsDelivery.Data.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> FindAllByOrderByNameAscAsync();
}
