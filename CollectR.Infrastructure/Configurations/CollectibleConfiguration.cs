using CollectR.Domain;
using CollectR.Domain.Collectibles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectR.Infrastructure.Configurations;

internal sealed class CollectibleConfiguration : IEntityTypeConfiguration<Collectible>
{
    public void Configure(EntityTypeBuilder<Collectible> builder)
    {
        builder
            .HasDiscriminator<string>("CollectibleType")
            .HasValue<Book>("Book")
            .HasValue<GameTitle>("GameTitle")
            .HasValue<AudioTape>("AudioTape")
            .HasValue<Item>("Item");

        builder
            .Property(c => c.Value)
            .HasColumnType("decimal(10, 2)");

        builder
            .HasOne(c => c.Category)
            .WithMany(cc => cc.Collectibles)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
