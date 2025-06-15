namespace CollectR.Application.Features.Collections.Queries.GetCollections;

internal sealed record GetCollectionsQueryResponse(
    int Id,
    string Name,
    string? Description,
    IEnumerable<int> CollectibleIds
);
