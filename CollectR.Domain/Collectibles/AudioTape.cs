namespace CollectR.Domain.Collectibles;

public class AudioTape : Collectible
{
    public string Artist { get; set; } = string.Empty;

    public string? Album { get; set; }

    public string? Label { get; set; }

    public int? ReleaseYear { get; set; }

    public string? Format { get; set; }

    public string? Genre { get; set; }

    public int? DurationMinutes { get; set; }

    public bool IsSigned { get; set; }
}
