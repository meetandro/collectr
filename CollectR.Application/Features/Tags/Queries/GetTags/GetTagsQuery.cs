using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;

namespace CollectR.Application.Features.Tags.Queries.GetTags;

public sealed record GetTagsQuery : IQuery<Result<IEnumerable<GetTagsQueryResponse>>>;
