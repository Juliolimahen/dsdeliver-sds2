using DsDelivery.Core.Domain;
using System.Linq.Expressions;

namespace DsDelivery.Data.Repositories;

public interface IProductRepository
{
    Task<Product> AddAsync(Product entity);
    Task<List<Product>> FindAllByOrderByNameAscAsync();
    Task<List<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<bool> RemoveAsync(Product entity);
    Task<Product> UpdateAsync(Product entity);
}
