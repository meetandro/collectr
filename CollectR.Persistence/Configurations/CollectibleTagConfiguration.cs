using CollectR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectR.Persistence.Configurations;

internal sealed class CollectibleTagConfiguration : IEntityTypeConfiguration<CollectibleTag>
{
    public void Configure(EntityTypeBuilder<CollectibleTag> builder)
    {
        builder.HasQueryFilter(ct => !ct.Collectible.IsDeleted);

        builder.HasKey(ct => new { ct.CollectibleId, ct.TagId });

        builder
            .HasOne(ct => ct.Collectible)
            .WithMany(c => c.CollectibleTags)
            .HasForeignKey(ct => ct.CollectibleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ct => ct.Tag)
            .WithMany(t => t.CollectibleTags)
            .HasForeignKey(ct => ct.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
