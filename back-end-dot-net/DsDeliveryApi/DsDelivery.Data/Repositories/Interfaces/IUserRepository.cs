using DsDelivery.Core.Domain;

namespace DsDelivery.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetAsync(string login);
    Task InsertUserFuncaoAsync(User user);
}
