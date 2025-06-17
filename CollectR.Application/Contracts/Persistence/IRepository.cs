namespace CollectR.Application.Contracts.Persistence;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity> CreateAsync(TEntity entity);

    Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);

    TEntity Update(TEntity entity);

    Task<bool> DeleteAsync(Guid id);
}
