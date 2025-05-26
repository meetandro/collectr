using CollectR.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CollectR.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
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
