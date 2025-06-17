using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public class ImageRepository(IApplicationDbContext context) : Repository<Image>(context), IImageRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<bool> HardDeleteAsync(Guid id)
    {
        var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
        if (image is not null)
        {
            _context.Images.Remove(image);
            return true;
        }
        return false;
    }
}
