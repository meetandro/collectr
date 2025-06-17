using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public sealed class CategoryRepository(IApplicationDbContext context)
    : Repository<Category>(context),
        ICategoryRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Category?> GetWithDetailsAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.Collectibles)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
