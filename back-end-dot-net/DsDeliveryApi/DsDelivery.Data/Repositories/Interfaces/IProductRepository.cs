using DsDelivery.Core.Domain;

namespace DsDelivery.Data.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> FindAllByOrderByNameAscAsync();
}
