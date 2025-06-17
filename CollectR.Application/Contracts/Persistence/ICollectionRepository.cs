using CollectR.Domain;

namespace CollectR.Application.Contracts.Persistence;

public interface ICollectionRepository : IRepository<Collection>
{
    Task<Collection?> GetWithDetailsAsync(Guid id);
}
