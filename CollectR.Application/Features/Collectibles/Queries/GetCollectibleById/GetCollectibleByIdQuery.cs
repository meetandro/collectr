using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

public sealed record GetCollectibleByIdQuery(Guid Id)
    : IQuery<Result<GetCollectibleByIdQueryResponse>>;
