using CollectR.Domain.Enums;

namespace CollectR.Application.Contracts.Models;

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
