using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Collectible> Collectibles { get; set; } = [];
}
