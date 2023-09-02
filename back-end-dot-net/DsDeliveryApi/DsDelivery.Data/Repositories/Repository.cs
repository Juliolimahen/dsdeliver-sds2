using DsDelivery.Core.Domain;
using DsDelivery.Data.Repositories.Interfaces;
using DsDeliveryApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DsDelivery.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    protected Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(int? id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> RemoveAsync(int? id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new Exception($"Entidade com o ID {id} não foi encontrada.");
        }
        var productRemoved = _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return productRemoved.Entity;
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
