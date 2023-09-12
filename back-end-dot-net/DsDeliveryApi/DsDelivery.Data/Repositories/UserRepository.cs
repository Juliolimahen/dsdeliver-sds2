using DsDelivery.Core.Domain;
using DsDelivery.Data.Repositories.Interfaces;
using DsDeliveryApi.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DsDelivery.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetAsync(string login)
        {
            return await _dbSet
                .Include(p => p.Positions)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Login == login);
        }

        public override async Task<User> AddAsync(User user)
        {
            await InsertUserFuncaoAsync(user);
            await _dbSet.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task InsertUserFuncaoAsync(User user)
        {
            var positionsConsulteds = new List<Position>();
            foreach (var funcao in user.Positions)
            {
                var funcaoConsulted = await _dbContext.Positions.FindAsync(funcao.Id);
                positionsConsulteds.Add(funcaoConsulted);
            }
            user.Positions = positionsConsulteds;
        }

        public override async Task<User> UpdateAsync(User user)
        {
            var userConsulted = await _dbSet.FindAsync(user.Login);

            if (userConsulted == null)
            {
                throw new Exception($"Usuário com login {user.Login} não encontrado.");
            }

            _dbContext.Entry(userConsulted).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();

            return userConsulted;
        }
    }
}
