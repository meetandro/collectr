using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Tags.Queries.GetTags;

internal sealed class GetTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetTagsQuery, IEnumerable<GetTagsQueryResponse>>
{
    public async Task<IEnumerable<GetTagsQueryResponse>> Handle(
        GetTagsQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context
            .Tags.AsNoTracking()
            .ProjectTo<GetTagsQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return result;
    }
}
