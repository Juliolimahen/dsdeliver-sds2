using DsDelivery.Core.Domain;
using DsDeliveryApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<User> _dbSet;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<User>();
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetAsync(string login)
        {
            return await _dbSet
                .Include(p => p.Positions)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Login == login);
        }

        public async Task<User> InsertAsync(User user)
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

        public async Task<User> UpdateAsync(User user)
        {
            var userConsulted = await _dbSet.FindAsync(user.Login);
            if (userConsulted == null)
            {
                return null;
            }
            _dbContext.Entry(userConsulted).CurrentValues.SetValues(user);
            await _dbContext.SaveChangesAsync();
            return userConsulted;
        }
    }
}
