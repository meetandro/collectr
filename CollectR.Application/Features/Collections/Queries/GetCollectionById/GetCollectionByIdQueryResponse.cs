namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

public sealed record GetCollectionByIdQueryResponse(
    Guid Id,
    string Name,
    string? Description,
    IEnumerable<Guid> CollectibleIds
);
