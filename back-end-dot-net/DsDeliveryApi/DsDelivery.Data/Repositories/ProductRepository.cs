using DsDelivery.Core.Domain;
using Microsoft.EntityFrameworkCore;
using DsDeliveryApi.Data.Context;
using DsDelivery.Data.Repositories.Interfaces;

namespace DsDelivery.Data.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Product>> FindAllByOrderByNameAscAsync()
    {
        List<Product> productList = await _dbSet.OrderBy(p => p.Name).ToListAsync();
        return productList;
    }
}