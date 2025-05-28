using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Infrastructure.Persistence.Repositories;

public class ImageRepository(ApplicationDbContext context) : IImageRepository
{
    public async Task<IEnumerable<Image>> CreateRangeAsync(IEnumerable<Image> images)
    {
        await context.Images.AddRangeAsync(images);
        await context.SaveChangesAsync();
        return images;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var image = await context.Images.FirstOrDefaultAsync(i => i.Id == id);
        if (image is not null)
        {
            context.Images.Remove(image);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
