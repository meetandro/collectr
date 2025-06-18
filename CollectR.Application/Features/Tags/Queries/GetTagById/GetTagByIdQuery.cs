using CollectR.Application.Common.Result;
using MediatR;

namespace CollectR.Application.Features.Tags.Queries.GetTagById;

public sealed record GetTagByIdQuery(Guid Id) : IRequest<Result<GetTagByIdQueryResponse>>;
