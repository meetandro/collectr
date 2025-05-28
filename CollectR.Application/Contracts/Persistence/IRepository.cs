namespace CollectR.Application.Contracts.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(int id);

    Task<TEntity> CreateAsync(TEntity entity);

    TEntity Update(TEntity entity);

    Task<bool> DeleteAsync(int id);
}
