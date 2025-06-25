using CollectR.Application.Contracts.Persistence;
using CollectR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public class Repository<TEntity>(IApplicationDbContext context) : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        List<TEntity> entities = await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync();
        return entities;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        if (entity is null)
        {
            return false;
        }
        _dbSet.Remove(entity);
        return true;
    }
}
