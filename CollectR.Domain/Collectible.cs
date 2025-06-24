using CollectR.Domain.Common;
using CollectR.Domain.Enums;

namespace CollectR.Domain;

public class Collectible : Entity
{
    public string Title { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string? Currency { get; init; }

    public decimal? Value { get; init; }

    public DateTime? AcquiredDate { get; init; }

    public bool IsCollected { get; init; } = false;

    public int SortIndex { get; init; } = 100;

    public Color? Color { get; init; }

    public Condition? Condition { get; init; }

    public Attributes? Attributes { get; init; }

    public Guid CategoryId { get; init; }
    public Category? Category { get; init; }

    public Guid CollectionId { get; init; }
    public Collection? Collection { get; init; }

    public ICollection<CollectibleTag> CollectibleTags { get; init; } = [];

    public ICollection<Image> Images { get; init; } = [];
}
