namespace CollectR.Application.Features.Tags.Queries.GetTags;

public sealed record GetTagsQueryResponse(
    Guid Id,
    string Name,
    string Hex,
    Guid CollectionId,
    IEnumerable<Guid> CollectibleIds
);
