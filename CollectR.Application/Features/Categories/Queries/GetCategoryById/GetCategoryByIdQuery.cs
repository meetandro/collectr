using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<Result<GetCategoryByIdQueryResponse>>;
