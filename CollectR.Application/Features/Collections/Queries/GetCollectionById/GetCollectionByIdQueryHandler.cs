using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

internal class GetCollectionByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetCollectionByIdQuery, GetCollectionByIdQueryResponse>
{
    public async Task<GetCollectionByIdQueryResponse> Handle(GetCollectionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Collections
            .Where(c => c.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetCollectionByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException();

        return result;
    }
}
