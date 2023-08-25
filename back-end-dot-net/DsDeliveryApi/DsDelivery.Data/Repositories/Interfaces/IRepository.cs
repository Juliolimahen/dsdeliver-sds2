using DsDelivery.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Data.Repositories.Interfaces
{
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
}
