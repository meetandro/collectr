using CollectR.Domain.Collectibles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectR.Infrastructure.Configurations;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .Property(i => i.Condition)
            .HasConversion<int>();

        builder
            .Property(i => i.Color)
            .HasConversion<int>();
    }
}
