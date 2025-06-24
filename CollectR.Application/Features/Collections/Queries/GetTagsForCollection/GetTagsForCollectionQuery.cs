using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collections.Queries.GetTagsForCollection;

public sealed record GetTagsForCollectionQuery(Guid Id)
    : IQuery<Result<IEnumerable<GetTagsForCollectionQueryResponse>>>;
