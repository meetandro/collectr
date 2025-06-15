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
    Guid AttributesId,
    Guid CategoryId,
    Guid CollectionId,
    IEnumerable<string> ImageUris,
    IEnumerable<Guid> TagIds
);
