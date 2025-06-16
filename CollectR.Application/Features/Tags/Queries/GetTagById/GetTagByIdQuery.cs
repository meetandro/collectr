using CollectR.Application.Common;
using MediatR;

namespace CollectR.Application.Features.Tags.Queries.GetTagById;

public sealed record GetTagByIdQuery(Guid Id) : IRequest<Result<GetTagByIdQueryResponse>>;
