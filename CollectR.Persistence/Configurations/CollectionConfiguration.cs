using CollectR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectR.Persistence.Configurations;

internal sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.Description).HasMaxLength(500);
    }
}
