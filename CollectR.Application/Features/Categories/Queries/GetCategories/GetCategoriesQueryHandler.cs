using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Categories.Queries.GetCategories;

internal sealed class GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCategoriesQuery, IEnumerable<GetCategoriesQueryResponse>>
{
    public async Task<IEnumerable<GetCategoriesQueryResponse>> Handle(
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
