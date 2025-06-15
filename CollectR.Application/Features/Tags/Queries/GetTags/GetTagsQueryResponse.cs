namespace CollectR.Application.Features.Tags.Queries.GetTags;

internal sealed record GetTagsQueryResponse(
    int Id,
    string Name,
    string Hex,
    Guid CollectionId,
    IEnumerable<Guid> CollectibleIds
);
