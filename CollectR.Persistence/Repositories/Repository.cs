using CollectR.Application.Contracts.Persistence;
using CollectR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(IApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id) // returning null a good idea? getbyids also does a reverse check
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
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

    public async Task<bool> DeleteAsync(Guid id)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var entities = await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync();
        return entities;
    }
}
