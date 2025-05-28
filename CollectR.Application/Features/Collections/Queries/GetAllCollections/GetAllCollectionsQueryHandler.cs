using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetAllCollections;

internal class GetAllCollectionsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllCollectionsQuery, IEnumerable<GetAllCollectionsQueryResponse>>
{
    public async Task<IEnumerable<GetAllCollectionsQueryResponse>> Handle(GetAllCollectionsQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Collections
            .AsNoTracking()
            .ProjectTo<GetAllCollectionsQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
