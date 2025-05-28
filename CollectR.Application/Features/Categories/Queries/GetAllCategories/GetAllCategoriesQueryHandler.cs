using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Categories.Queries.GetAllCategories;

internal class GetAllCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetAllCategoriesQueryResponse>>
{
    public async Task<IEnumerable<GetAllCategoriesQueryResponse>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Categories
            .AsNoTracking()
            .ProjectTo<GetAllCategoriesQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
