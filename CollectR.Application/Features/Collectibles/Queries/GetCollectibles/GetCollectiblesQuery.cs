using CollectR.Application.Abstractions;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibles;

public sealed record GetCollectiblesQuery : IQuery<IEnumerable<GetCollectiblesQueryResponse>>;
