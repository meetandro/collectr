using MediatR;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

public sealed record GetCollectibleByIdQuery(int Id) : IRequest<GetCollectibleByIdQueryResponse>;
