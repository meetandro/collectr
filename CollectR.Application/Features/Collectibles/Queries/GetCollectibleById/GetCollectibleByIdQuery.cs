using CollectR.Application.Abstractions;
using CollectR.Application.Common;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

public sealed record GetCollectibleByIdQuery(Guid Id)
    : IQuery<Result<GetCollectibleByIdQueryResponse>>;
