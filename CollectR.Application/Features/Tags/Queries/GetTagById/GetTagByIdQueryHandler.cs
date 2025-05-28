using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Tags.Queries.GetTagById;

internal class GetTagByIdQueryHandler(ITagRepository tagRepository, IMapper mapper) : IRequestHandler<GetTagByIdQuery, GetTagByIdQueryResponse>
{
    public async Task<GetTagByIdQueryResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.Id)
            ?? throw new NotImplementedException(); // !!! custom exception

        var result = mapper.Map<GetTagByIdQueryResponse>(tag); // !!! make sure result is actualy the result (check getAllqueries where you DONT return the result!!!)

        return result;
    }
}
