using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetTagsForCollection;

internal sealed class GetTagsForCollectionQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTagsForCollectionQuery, IEnumerable<GetTagsForCollectionQueryResponse>>
{
    public async Task<IEnumerable<GetTagsForCollectionQueryResponse>> Handle(
        GetTagsForCollectionQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context
            .Tags.Where(t => t.CollectionId == request.Id)
            .Include(t => t.CollectibleTags)
            .AsNoTracking()
            .ProjectTo<GetTagsForCollectionQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return result;
    }
}
