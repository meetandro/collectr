namespace CollectR.Application.Features.Collections.Queries.GetTagsForCollection;

public record GetTagsForCollectionQueryResponse(
    Guid Id,
    string Name,
    string Hex,
    Guid CollectionId,
    IEnumerable<Guid> CollectibleIds
);
