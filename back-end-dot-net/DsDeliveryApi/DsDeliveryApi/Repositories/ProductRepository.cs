using DsDeliveryApi.Data;
using DsDeliveryApi.Models;
using DsDeliveryApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DsDeliveryApi.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Product>();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> RemoveAsync(Product entity)
        {
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Product>> FindAllByOrderByNameAscAsync()
        {
            List<Product> productList = await _dbSet.OrderBy(p => p.Name).ToListAsync();
            return productList;
        }

        public List<Order> FindOrdersWithProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}