using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Categories.Queries.GetCategories;

internal sealed class GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCategoriesQuery, Result<IEnumerable<GetCategoriesQueryResponse>>>
{
    public async Task<Result<IEnumerable<GetCategoriesQueryResponse>>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Categories
            .AsNoTracking()
            .ProjectTo<GetCategoriesQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
