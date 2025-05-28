using MediatR;

namespace CollectR.Application.Features.Collections.Queries.GetAllCollections;

public sealed record GetAllCollectionsQuery() : IRequest<IEnumerable<GetAllCollectionsQueryResponse>>;
