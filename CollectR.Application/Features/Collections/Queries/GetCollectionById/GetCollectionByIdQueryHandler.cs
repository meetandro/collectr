using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

internal sealed class GetCollectionByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCollectionByIdQuery, Result<GetCollectionByIdQueryResponse>>
{
    public async Task<Result<GetCollectionByIdQueryResponse>> Handle(
        GetCollectionByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context
            .Collections.Where(c => c.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetCollectionByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        return result;
    }
}
