namespace CollectR.Application.Features.Collections.Queries.GetCollections;

public sealed record GetCollectionsQueryResponse(
    Guid Id,
    string Name,
    string? Description,
    IEnumerable<Guid> CollectibleIds
);
