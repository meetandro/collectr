using CollectR.Domain.Common;

namespace CollectR.Domain;

public class Image : Entity
{
    public string Uri { get; init; } = string.Empty;

    public string? Alt { get; init; }

    public Guid CollectibleId { get; init; }
    public Collectible? Collectible { get; init; }
}
