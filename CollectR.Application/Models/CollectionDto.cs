namespace CollectR.Application.Models;

public sealed class CollectionDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<CollectibleDto> Collectibles { get; set; } = [];
}
