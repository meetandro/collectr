using CollectR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectR.Persistence.Configurations;

internal sealed class CollectibleConfiguration : IEntityTypeConfiguration<Collectible>
{
    public void Configure(EntityTypeBuilder<Collectible> builder)
    {
        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.Property(c => c.Title).HasMaxLength(100);

        builder.Property(c => c.Description).HasMaxLength(1000);

        builder.Property(c => c.Currency).HasMaxLength(100);

        builder.Property(c => c.Value).HasColumnType("decimal(10, 2)");

        builder.Property(c => c.Condition).HasConversion<int>();

        builder.Property(c => c.Color).HasConversion<int>();

        builder.OwnsOne(c => c.Attributes);

        builder
            .HasOne(c => c.Category)
            .WithMany(c => c.Collectibles)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
