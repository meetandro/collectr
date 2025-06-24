using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Category : Entity
{
    public string Name { get; init; } = string.Empty;

    public ICollection<Collectible> Collectibles { get; init; } = [];
}
