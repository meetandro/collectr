using CollectR.Application.Abstractions.Messaging;

namespace CollectR.Application.Features.Tags.Queries.GetTags;

public sealed record GetTagsQuery : IQuery<IEnumerable<GetTagsQueryResponse>>;
