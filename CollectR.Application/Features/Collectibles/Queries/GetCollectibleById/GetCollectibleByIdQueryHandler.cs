using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

internal class GetCollectibleByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetCollectibleByIdQuery, GetCollectibleByIdQueryResponse>
{
    public async Task<GetCollectibleByIdQueryResponse> Handle(GetCollectibleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Collectibles
            .Where(c => c.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetCollectibleByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException();

        return result;
    }
}
