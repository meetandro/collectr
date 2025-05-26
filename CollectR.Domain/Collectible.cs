using CollectR.Domain.Common;

namespace CollectR.Domain;

public abstract class Collectible : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Currency { get; set; }

    public decimal? Value { get; set; }

    public DateTime? AcquiredDate { get; set; }

    public bool? IsCollected { get; set; }

    public int SortIndex { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public int CollectionId { get; set; }

    public Collection? Collection { get; set; }

    public ICollection<CollectibleTag> CollectibleTags { get; set; } = [];

    public ICollection<Image> Images { get; set; } = [];
}
