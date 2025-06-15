using CollectR.Domain.Common;
using CollectR.Domain.Enums;

namespace CollectR.Domain;

public class Collectible : Entity
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Currency { get; set; }

    public decimal? Value { get; set; }

    public DateTime? AcquiredDate { get; set; }

    public bool? IsCollected { get; set; }

    public int? SortIndex { get; set; }

    public Color? Color { get; set; }

    public Condition? Condition { get; set; }

    public Guid AttributesId { get; set; }

    public Attributes? Attributes { get; set; }

    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }

    public Guid CollectionId { get; set; }

    public Collection? Collection { get; set; }

    public ICollection<CollectibleTag> CollectibleTags { get; set; } = [];

    public ICollection<Image> Images { get; set; } = [];
}
