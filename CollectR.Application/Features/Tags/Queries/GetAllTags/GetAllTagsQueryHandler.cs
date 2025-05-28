using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Tags.Queries.GetAllTags;

internal class GetAllTagsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllTagsQuery, IEnumerable<GetAllTagsQueryResponse>>
{
    public async Task<IEnumerable<GetAllTagsQueryResponse>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Tags
            .AsNoTracking()
            .ProjectTo<GetAllTagsQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
