using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Tag : Entity
{
    public string Name { get; set; } = string.Empty;

    public string Hex { get; set; } = "#FFFFFF";

    public Guid CollectionId { get; set; }
    public Collection? Collection { get; set; }

    public ICollection<CollectibleTag> CollectibleTags { get; set; } = [];
}
