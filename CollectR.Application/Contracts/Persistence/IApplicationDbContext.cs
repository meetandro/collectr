using CollectR.Domain;
using CollectR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Contracts.Persistence;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity;

    public DbSet<Category> Categories { get; }

    public DbSet<Collectible> Collectibles { get; }

    public DbSet<Collection> Collections { get; }

    public DbSet<Image> Images { get; }

    public DbSet<Tag> Tags { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
