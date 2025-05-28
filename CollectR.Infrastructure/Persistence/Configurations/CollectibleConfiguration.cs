using CollectR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectR.Infrastructure.Persistence.Configurations;

internal sealed class CollectibleConfiguration : IEntityTypeConfiguration<Collectible>
{
    public void Configure(EntityTypeBuilder<Collectible> builder)
    {
        builder
            .Property(c => c.Value)
            .HasColumnType("decimal(10, 2)");

        builder
            .Property(i => i.Condition)
            .HasConversion<int>();

        builder
            .Property(i => i.Color)
            .HasConversion<int>();

        builder
            .HasOne(c => c.Category)
            .WithMany(cc => cc.Collectibles)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
