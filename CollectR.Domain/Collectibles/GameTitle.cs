namespace CollectR.Domain.Collectibles;

public class GameTitle : Collectible
{
    public string Developer { get; set; } = string.Empty;

    public string? Publisher { get; set; }

    public string? Platform { get; set; }

    public int? ReleaseYear { get; set; }

    public string? Genre { get; set; }

    public string? Format { get; set; }

    public string? Rating { get; set; }
}
