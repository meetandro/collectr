using CollectR.Domain;

namespace CollectR.Application.Contracts.Persistence;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetWithDetailsAsync(Guid id);
}
