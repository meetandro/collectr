using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

public sealed record GetCollectionByIdQuery(Guid Id)
    : IQuery<Result<GetCollectionByIdQueryResponse>>;
