using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Contracts.Persistence;

public interface IApplicationDbContext
{
    public DbSet<Category> Categories { get; }

    public DbSet<Collectible> Collectibles { get; }

    public DbSet<Collection> Collections { get; }

    public DbSet<Image> Images { get; }

    public DbSet<Tag> Tags { get; }
}
