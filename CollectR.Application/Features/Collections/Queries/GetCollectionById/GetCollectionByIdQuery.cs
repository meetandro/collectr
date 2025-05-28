using MediatR;

namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

public sealed record GetCollectionByIdQuery(int Id) : IRequest<GetCollectionByIdQueryResponse>;
