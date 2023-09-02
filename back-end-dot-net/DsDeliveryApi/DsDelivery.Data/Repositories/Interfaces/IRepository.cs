using DsDelivery.Core.Domain;
using System.Linq.Expressions;


namespace DsDelivery.Data.Repositories.Interfaces;

public interface IRepository<T> : IDisposable where T : Entity
{
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int? id);
    Task<T> UpdateAsync(T entity);
    Task<T> RemoveAsync(int? id);

    Task<IEnumerable<T>>
        FindAsync(Expression<Func<T, bool>> predicate);
}
