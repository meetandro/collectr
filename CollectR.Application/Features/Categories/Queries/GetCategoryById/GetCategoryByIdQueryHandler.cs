using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Categories.Queries.GetCategoryById;

internal class GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    public async Task<GetCategoryByIdQueryResponse> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Categories
            .Where(c => c.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetCategoryByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException();

        return result;
    }
}
