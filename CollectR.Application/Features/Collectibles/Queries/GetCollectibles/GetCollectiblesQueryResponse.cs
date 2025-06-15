using CollectR.Domain.Enums;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibles;

internal sealed record GetCollectiblesQueryResponse(
    Guid Id, // return ids elsewhere aswell for all entities
    string Title,
    string? Description,
    string? Currency,
    decimal? Value,
    DateTime? AcquiredDate,
    bool? IsCollected,
    int? SortIndex,
    Color? Color,
    Condition? Condition,
    Guid AttributesId,
    Guid CategoryId,
    Guid CollectionId,
    IEnumerable<string> ImageUris,
    IEnumerable<Guid> TagIds
);
