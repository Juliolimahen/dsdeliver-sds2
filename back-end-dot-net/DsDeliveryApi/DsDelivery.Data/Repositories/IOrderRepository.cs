﻿using DsDelivery.Core.Domain;
using System.Linq.Expressions;

namespace DsDelivery.Data.Repositories;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order entity);
    Task<List<Order>> FindAsync(Expression<Func<Order, bool>> predicate);
    Task<List<Order>> FindOrdersWithProducts();
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task<Order> RemoveAsync(int id);
    Task<Order> UpdateAsync(Order entity);
}
