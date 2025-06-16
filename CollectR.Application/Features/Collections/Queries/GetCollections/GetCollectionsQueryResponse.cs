namespace CollectR.Application.Features.Collections.Queries.GetCollections;

internal sealed record GetCollectionsQueryResponse(
    Guid Id,
    string Name,
    string? Description,
    IEnumerable<Guid> CollectibleIds
);
