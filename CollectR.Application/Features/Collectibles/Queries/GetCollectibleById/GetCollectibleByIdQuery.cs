using CollectR.Application.Abstractions;
using MediatR;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

public sealed record GetCollectibleByIdQuery(Guid Id)
    : IRequest<Result<GetCollectibleByIdQueryResponse>>;
