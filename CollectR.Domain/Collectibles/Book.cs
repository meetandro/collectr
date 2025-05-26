namespace CollectR.Domain.Collectibles;

public class Book : Collectible
{
    public string Author { get; set; } = string.Empty;

    public string? Publisher { get; set; }

    public int? PageCount { get; set; }

    public string? ISBN { get; set; }

    public string? Language { get; set; }

    public int? PublishedYear { get; set; }

    public string? Genre { get; set; }

    public string? Edition { get; set; }

    public string? Format { get; set; }
}
