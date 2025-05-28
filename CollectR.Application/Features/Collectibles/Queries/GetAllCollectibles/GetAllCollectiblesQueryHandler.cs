using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Queries.GetAllCollectibles;

internal class GetAllCollectiblesQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllCollectiblesQuery, IEnumerable<GetAllCollectiblesQueryResponse>>
{
    public async Task<IEnumerable<GetAllCollectiblesQueryResponse>> Handle(GetAllCollectiblesQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Collectibles
            .AsNoTracking()
            .ProjectTo<GetAllCollectiblesQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
