using DsDelivery.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetAsync(string login);
        Task<User> InsertAsync(User user);
        Task InsertUserFuncaoAsync(User user);
        Task<User> UpdateAsync(User user);
    }
}
