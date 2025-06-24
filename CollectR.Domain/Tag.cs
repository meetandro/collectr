using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Tag : Entity
{
    public string Name { get; init; } = string.Empty;

    public string Hex { get; init; } = "#FFFFFF";

    public Guid CollectionId { get; init; }
    public Collection? Collection { get; init; }

    public ICollection<CollectibleTag> CollectibleTags { get; init; } = [];
}
