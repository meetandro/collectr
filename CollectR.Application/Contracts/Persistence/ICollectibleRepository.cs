using CollectR.Domain;

namespace CollectR.Application.Contracts.Persistence;

public interface ICollectibleRepository : IRepository<Collectible>
{
    Task<Collectible?> GetWithDetailsAsync(Guid id);

    IQueryable<Collectible> GetFilteredQueryableForCollection(
        Guid collectionId,
        string? searchQuery = null,
        string? colors = null,
        string? currency = null,
        decimal? minValue = null,
        decimal? maxValue = null,
        string? conditions = null,
        string? categories = null,
        string? tags = null,
        DateTime? acquiredFrom = null,
        DateTime? acquiredTo = null,
        bool? isCollected = null,
        string? sortBy = null,
        string? sortOrder = null
    );
}
