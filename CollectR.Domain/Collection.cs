using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Collection : Entity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Collectible> Collectibles { get; set; } = [];

    public ICollection<Tag> Tags { get; set; } = [];
}
