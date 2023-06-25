﻿using DsDeliveryApi.Data;
using DsDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public List<Order> FindOrdersWithProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetByIdAsync(long id)
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