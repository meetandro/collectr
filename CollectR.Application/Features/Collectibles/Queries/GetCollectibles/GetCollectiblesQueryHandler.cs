using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibles;

internal sealed class GetCollectiblesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCollectiblesQuery, IEnumerable<GetCollectiblesQueryResponse>>
{
    public async Task<IEnumerable<GetCollectiblesQueryResponse>> Handle(
        GetCollectiblesQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Collectibles
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<GetCollectiblesQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
