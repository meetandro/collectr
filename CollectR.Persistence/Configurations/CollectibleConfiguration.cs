using CollectR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectR.Persistence.Configurations;

internal sealed class CollectibleConfiguration : IEntityTypeConfiguration<Collectible>
{
    public void Configure(EntityTypeBuilder<Collectible> builder)
    {
        builder.Property(c => c.Value).HasColumnType("decimal(10, 2)");

        builder.Property(i => i.Condition).HasConversion<int>();

        builder.Property(i => i.Color).HasConversion<int>();

        builder
            .HasOne(c => c.Category)
            .WithMany(cc => cc.Collectibles)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.Attributes)
            .WithOne(a => a.Collectible)
            .HasForeignKey<Attributes>(a => a.CollectibleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
