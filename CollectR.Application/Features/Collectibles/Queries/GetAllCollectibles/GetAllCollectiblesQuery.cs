using MediatR;

namespace CollectR.Application.Features.Collectibles.Queries.GetAllCollectibles;

public sealed record GetAllCollectiblesQuery() : IRequest<IEnumerable<GetAllCollectiblesQueryResponse>>;
