using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using CollectR.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public sealed class CollectibleRespository(IApplicationDbContext context)
    : Repository<Collectible>(context),
        ICollectibleRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Collectible?> GetWithDetailsAsync(Guid id)
    {
        return await _context
            .Collectibles.Include(c => c.Images)
            .Include(c => c.CollectibleTags)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public IQueryable<Collectible> GetFilteredQueryableForCollection(
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
    )
    {
        return _context
            .Collectibles.Where(c => c.CollectionId == collectionId)
            .WhereSearchQuery(searchQuery)
            .WhereColors(colors)
            .WhereCurrency(currency)
            .WhereMinValue(minValue)
            .WhereMaxValue(maxValue)
            .WhereConditions(conditions)
            .WhereCategories(categories)
            .WhereTags(tags)
            .WhereIsCollected(isCollected)
            .WhereAcquiredFrom(acquiredFrom)
            .WhereAcquiredTo(acquiredTo)
            .OrderBySort(sortBy, sortOrder);
    }
}
