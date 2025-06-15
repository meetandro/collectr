using System.Reflection;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using CollectR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options),
        IApplicationDbContext
{
    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity => base.Set<TEntity>();

    public DbSet<Attributes> Attributes { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Collectible> Collectibles { get; set; }

    public DbSet<Collection> Collections { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
