using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

internal sealed class GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCategoryByIdQuery, Result<GetCategoryByIdQueryResponse>>
{
    public async Task<Result<GetCategoryByIdQueryResponse>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Categories
            .Where(c => c.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetCategoryByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        return result;
    }
}
