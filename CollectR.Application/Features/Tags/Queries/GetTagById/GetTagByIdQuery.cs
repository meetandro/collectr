using MediatR;

namespace CollectR.Application.Features.Tags.Queries.GetTagById;

public sealed record GetTagByIdQuery(int Id) : IRequest<GetTagByIdQueryResponse>;
