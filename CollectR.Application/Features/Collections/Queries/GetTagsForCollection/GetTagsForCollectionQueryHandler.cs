using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetTagsForCollection;

internal sealed class GetTagsForCollectionQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTagsForCollectionQuery, Result<IEnumerable<GetTagsForCollectionQueryResponse>>>
{
    public async Task<Result<IEnumerable<GetTagsForCollectionQueryResponse>>> Handle(
        GetTagsForCollectionQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Tags
            .Where(t => t.CollectionId == request.Id)
            .AsNoTracking()
            .ProjectTo<GetTagsForCollectionQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
