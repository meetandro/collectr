namespace CollectR.Domain;

public class CollectibleTag
{
    public Guid CollectibleId { get; init; }
    public Collectible? Collectible { get; init; }

    public Guid TagId { get; init; }
    public Tag? Tag { get; init; }
}
