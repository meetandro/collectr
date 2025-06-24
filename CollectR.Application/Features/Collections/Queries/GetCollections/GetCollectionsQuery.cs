using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collections.Queries.GetCollections;

public sealed record GetCollectionsQuery : IQuery<Result<IEnumerable<GetCollectionsQueryResponse>>>;
