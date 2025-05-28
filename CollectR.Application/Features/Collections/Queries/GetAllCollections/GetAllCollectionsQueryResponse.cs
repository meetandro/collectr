namespace CollectR.Application.Features.Collections.Queries.GetAllCollections;

internal sealed record GetAllCollectionsQueryResponse(int Id, string Name, string? Description, IEnumerable<int> CollectibleIds);
