using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence.Repositories;

public class ImageRepository(IApplicationDbContext context)
    : Repository<Image>(context),
        IImageRepository
{
    public async Task<bool> HardDeleteAsync(Guid id)
    {
        var image = await context.Images.FirstOrDefaultAsync(i => i.Id == id);
        if (image is not null)
        {
            context.Images.Remove(image);
            return true;
        }
        return false;
    }
}
