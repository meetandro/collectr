using CollectR.Application.Contracts.Persistence;
using CollectR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            return entity;
        }
        return null;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow; // handle accordingly
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return entities;
    }

    public TEntity Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }
        return false;
    }
}
