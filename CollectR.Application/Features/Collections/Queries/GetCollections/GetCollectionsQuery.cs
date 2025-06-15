using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Collections.Queries.GetCollections;

public sealed record GetCollectionsQuery : IQuery<IEnumerable<GetCollectionsQueryResponse>>;
