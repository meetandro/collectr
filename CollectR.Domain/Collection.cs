using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Collection : Entity
{
    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public ICollection<Collectible> Collectibles { get; init; } = [];

    public ICollection<Tag> Tags { get; init; } = [];
}
