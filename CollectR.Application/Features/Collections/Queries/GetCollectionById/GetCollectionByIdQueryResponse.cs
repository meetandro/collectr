namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

internal sealed record GetCollectionByIdQueryResponse(int Id, string Name, string? Description, IEnumerable<int>? CollectibleIds);
