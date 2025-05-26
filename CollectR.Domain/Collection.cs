using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Collection : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Collectible> Collectibles { get; set; } = [];
}
