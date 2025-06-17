using CollectR.Domain.Enums;

namespace CollectR.Application.Features.Collections.Queries.ExportCollection;

public sealed class CollectionDto // move this somewhere
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<CollectibleDto> Collectibles { get; set; } = [];
}

public sealed class CollectibleDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Currency { get; set; }
    public decimal? Value { get; set; }
    public DateTime? AcquiredDate { get; set; }
    public bool IsCollected { get; set; }
    public int SortIndex { get; set; }
    public Color? Color { get; set; }
    public Condition? Condition { get; set; }
    public string? Metadata { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<TagDto> Tags { get; set; } = [];
}

public sealed class TagDto
{
    public string Name { get; set; } = string.Empty;
    public string Hex { get; set; } = string.Empty;
}
