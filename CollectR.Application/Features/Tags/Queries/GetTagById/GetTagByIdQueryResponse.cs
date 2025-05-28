namespace CollectR.Application.Features.Tags.Queries.GetTagById;

internal sealed record GetTagByIdQueryResponse(int Id, string Name, string Hex, int CollectionId, IEnumerable<int>? CollectibleTagIds);
