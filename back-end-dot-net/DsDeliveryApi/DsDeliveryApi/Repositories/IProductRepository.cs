﻿using DsDeliveryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DsDeliveryApi.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product entity);
        Task<List<Product>> FindAllByOrderByNameAsc();
        Task<List<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        List<Order> FindOrdersWithProducts();
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(long id);
        Task<bool> RemoveAsync(Product entity);
        Task<Product> UpdateAsync(Product entity);
    }
}