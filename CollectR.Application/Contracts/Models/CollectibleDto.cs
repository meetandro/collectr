using CollectR.Domain.Enums;

namespace CollectR.Application.Contracts.Models;

public sealed class CollectibleDto
{
    public string Title { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string? Currency { get; init; }

    public decimal? Value { get; init; }

    public DateTime? AcquiredDate { get; init; }

    public bool IsCollected { get; init; }

    public int SortIndex { get; init; }

    public Color? Color { get; init; }

    public Condition? Condition { get; init; }

    public string? Metadata { get; init; }

    public string Category { get; init; } = string.Empty;

    public List<TagDto> Tags { get; init; } = [];
}
