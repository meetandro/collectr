using CollectR.Domain;

namespace CollectR.Application.Contracts.Persistence;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetWithDetailsAsync(Guid id);
}
