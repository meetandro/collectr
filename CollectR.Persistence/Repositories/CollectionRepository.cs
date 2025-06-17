using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public sealed class CollectionRepository(IApplicationDbContext context)
    : Repository<Collection>(context),
        ICollectionRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Collection?> GetWithDetailsAsync(Guid id)
    {
        return await _context.Collections
            .Include(c => c.Tags)
            .Include(c => c.Collectibles)
                .ThenInclude(c => c.Images)
            .Include(c => c.Collectibles)
                .ThenInclude(c => c.CollectibleTags)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
