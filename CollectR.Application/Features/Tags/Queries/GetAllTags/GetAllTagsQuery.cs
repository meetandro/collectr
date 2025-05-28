using MediatR;

namespace CollectR.Application.Features.Tags.Queries.GetAllTags;

public sealed record GetAllTagsQuery() : IRequest<IEnumerable<GetAllTagsQueryResponse>>;
