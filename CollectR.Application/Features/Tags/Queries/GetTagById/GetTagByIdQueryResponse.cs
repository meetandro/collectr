namespace CollectR.Application.Features.Tags.Queries.GetTagById;

internal sealed record GetTagByIdQueryResponse(
    Guid Id,
    string Name,
    string Hex,
    Guid CollectionId,
    IEnumerable<Guid> CollectibleIds
);
