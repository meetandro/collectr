namespace CollectR.Domain;

public class CollectibleTag
{
    public int CollectibleId { get; set; }

    // [JsonIgnore]
    public Collectible? Collectible { get; set; }

    public int TagId { get; set; }

    public Tag? Tag { get; set; }
}
