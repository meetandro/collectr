namespace CollectR.Domain;

public class CollectibleTag
{
    public Guid CollectibleId { get; set; }
    public Collectible? Collectible { get; set; }

    public Guid TagId { get; set; }
    public Tag? Tag { get; set; }
}
