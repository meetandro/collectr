using CollectR.Domain.Enums;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

internal sealed record GetCollectibleByIdQueryResponse(
    string Title,
    string? Description,
    string? Currency,
    decimal? Value,
    DateTime? AcquiredDate,
    bool? IsCollected,
    int SortIndex,
    Color? Color,
    Condition? Condition,
    string? Metadata,
    int CategoryId,
    int CollectionId,
    IEnumerable<string> ImageUris,
    IEnumerable<int> TagIds
);
