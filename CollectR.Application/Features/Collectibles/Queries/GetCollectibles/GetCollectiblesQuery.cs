using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibles;

public sealed record GetCollectiblesQuery : IQuery<Result<IEnumerable<GetCollectiblesQueryResponse>>>;
