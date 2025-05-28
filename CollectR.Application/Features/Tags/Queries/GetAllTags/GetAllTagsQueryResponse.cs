namespace CollectR.Application.Features.Tags.Queries.GetAllTags;

internal sealed record GetAllTagsQueryResponse(int Id, string Name, string Hex, int CollectionId); // IEnumerable<int>? CollectibleTagIds
