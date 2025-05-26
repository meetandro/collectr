using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        TEntity entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public async Task<int> CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return 0;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        TEntity entity = await _dbSet.FindAsync(id);
        if (entity != null)
            _dbSet.Remove(entity);
        await SaveChangesAsync();
        return true;
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
