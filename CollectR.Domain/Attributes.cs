namespace CollectR.Domain;

public class Attributes
{
    public Guid Id { get; set; }

    public string Metadata { get; set; } = string.Empty;

    public Guid CollectibleId { get; set; }

    public Collectible Collectible { get; set; } = default!;
}
