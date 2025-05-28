using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Tags.Queries.GetAllTags;

internal class GetAllTagsQueryHandler(ITagRepository tagRepository, IMapper mapper) : IRequestHandler<GetAllTagsQuery, IEnumerable<GetAllTagsQueryResponse>>
{
    public async Task<IEnumerable<GetAllTagsQueryResponse>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetAllAsync();

        return tags.Select(mapper.Map<GetAllTagsQueryResponse>);
    }
}
