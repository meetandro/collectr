using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public sealed class TagRepository(IApplicationDbContext context)
    : Repository<Tag>(context),
        ITagRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Tag?> GetWithDetailsAsync(Guid id)
    {
        return await _context.Tags
            .Include(t => t.CollectibleTags)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
