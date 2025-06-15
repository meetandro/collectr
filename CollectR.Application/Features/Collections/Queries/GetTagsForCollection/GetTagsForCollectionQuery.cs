using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collections.Queries.GetTagsForCollection;

public sealed record GetTagsForCollectionQuery(Guid Id)
    : IQuery<IEnumerable<GetTagsForCollectionQueryResponse>>;
