namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

internal sealed record GetCollectionByIdQueryResponse(
    Guid Id,
    string Name,
    string? Description,
    IEnumerable<Guid> CollectibleIds
);
