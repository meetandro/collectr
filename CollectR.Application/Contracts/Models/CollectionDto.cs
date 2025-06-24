namespace CollectR.Application.Contracts.Models;

public sealed class CollectionDto
{
    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public List<CollectibleDto> Collectibles { get; init; } = [];
}
