using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

public sealed record GetCollectionByIdQuery(Guid Id)
    : IQuery<Result<GetCollectionByIdQueryResponse>>;
