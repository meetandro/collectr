using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Abstractions;
using CollectR.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

internal sealed class GetCollectibleByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetCollectibleByIdQuery, Result<GetCollectibleByIdQueryResponse>>
{
    public async Task<Result<GetCollectibleByIdQueryResponse>> Handle(
        GetCollectibleByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context
            .Collectibles.Where(c => c.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetCollectibleByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        return result;
    }
}
