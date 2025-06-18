using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

internal sealed class GetCollectibleByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCollectibleByIdQuery, Result<GetCollectibleByIdQueryResponse>>
{
    public async Task<Result<GetCollectibleByIdQueryResponse>> Handle(
        GetCollectibleByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Collectibles
            .Where(c => c.Id == request.Id)
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<GetCollectibleByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        return result;
    }
}
