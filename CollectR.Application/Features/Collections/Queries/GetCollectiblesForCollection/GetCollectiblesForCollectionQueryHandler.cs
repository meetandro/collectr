using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetCollectiblesForCollection;

internal sealed class GetCollectiblesForCollectionQueryHandler(
    ICollectibleRepository collectibleRepository,
    IMapper mapper
)
    : IQueryHandler<
        GetCollectiblesForCollectionQuery,
        Result<PaginatedList<GetCollectiblesForCollectionQueryResponse>>
    >
{
    public async Task<Result<PaginatedList<GetCollectiblesForCollectionQueryResponse>>> Handle(
        GetCollectiblesForCollectionQuery request,
        CancellationToken cancellationToken
    )
    {
        var collectibles = collectibleRepository
            .GetFilteredQueryableForCollection(
                request.Id,
                request.SearchQuery,
                request.Colors,
                request.Currency,
                request.MinValue,
                request.MaxValue,
                request.Conditions,
                request.Categories,
                request.Tags,
                request.AcquiredFrom,
                request.AcquiredTo,
                request.IsCollected,
                request.SortBy,
                request.SortOrder
            )
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<GetCollectiblesForCollectionQueryResponse>(mapper.ConfigurationProvider);

        var count = await collectibles.CountAsync(cancellationToken);

        var result = new PaginatedList<GetCollectiblesForCollectionQueryResponse>(
            await collectibles
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken),
            count,
            request.Page,
            request.PageSize
        );

        return result;
    }
}
