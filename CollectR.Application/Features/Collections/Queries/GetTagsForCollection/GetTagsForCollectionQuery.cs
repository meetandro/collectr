using CollectR.Application.Abstractions;

namespace CollectR.Application.Features.Collections.Queries.GetTagsForCollection;

public sealed record GetTagsForCollectionQuery(Guid Id)
    : IQuery<IEnumerable<GetTagsForCollectionQueryResponse>>;
