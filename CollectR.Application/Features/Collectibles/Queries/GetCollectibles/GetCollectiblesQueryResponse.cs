using CollectR.Domain.Enums;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibles;

public sealed record GetCollectiblesQueryResponse(
    Guid Id,
    string Title,
    string? Description,
    string? Currency,
    decimal? Value,
    DateTime? AcquiredDate,
    bool? IsCollected,
    int? SortIndex,
    Color? Color,
    Condition? Condition,
    string Metadata,
    Guid CategoryId,
    Guid CollectionId,
    IEnumerable<string> ImageUris,
    IEnumerable<Guid> TagIds
);
