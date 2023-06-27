using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DsDelivery.Core.Domain;
using DsDeliveryApi.Data;

namespace DsDeliveryApi.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Order> _dbSet;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Order>();
        }

        public async Task< List<Order>> FindOrdersWithProducts()
        {
            return await _dbContext.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<Order> AddAsync(Order entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> RemoveAsync(Order entity)
        {
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}