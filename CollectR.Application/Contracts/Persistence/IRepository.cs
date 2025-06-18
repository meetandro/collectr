namespace CollectR.Application.Contracts.Persistence;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity> CreateAsync(TEntity entity);

    TEntity Update(TEntity entity);

    Task<bool> DeleteAsync(Guid id);
}
