using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibles;

public sealed record GetCollectiblesQuery : IQuery<IEnumerable<GetCollectiblesQueryResponse>>;
