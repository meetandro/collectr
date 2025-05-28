using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Tags.Queries.GetTagById;

internal class GetTagByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetTagByIdQuery, GetTagByIdQueryResponse>
{
    public async Task<GetTagByIdQueryResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Tags
            .Where(t => t.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetTagByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException();

        return result;
    }
}
