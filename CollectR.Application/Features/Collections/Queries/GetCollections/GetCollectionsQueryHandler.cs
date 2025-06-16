using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetCollections;

internal sealed class GetCollectionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCollectionsQuery, IEnumerable<GetCollectionsQueryResponse>>
{
    public async Task<IEnumerable<GetCollectionsQueryResponse>> Handle(
        GetCollectionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context
            .Collections.AsNoTracking()
            .ProjectTo<GetCollectionsQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return result;
    }
}
