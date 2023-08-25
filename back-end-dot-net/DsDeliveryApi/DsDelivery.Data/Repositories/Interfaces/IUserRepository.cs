using DsDelivery.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(string login);
        Task InsertUserFuncaoAsync(User user);
    }
}
