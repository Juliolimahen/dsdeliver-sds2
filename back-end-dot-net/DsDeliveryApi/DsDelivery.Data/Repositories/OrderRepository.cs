using Microsoft.EntityFrameworkCore;
using DsDelivery.Core.Domain;
using DsDeliveryApi.Data.Context;
using DsDelivery.Data.Repositories.Interfaces;

namespace DsDelivery.Data.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Order>> FindOrdersWithProducts()
    {
        return await _dbContext.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ToListAsync();
    }
}
