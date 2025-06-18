using AutoMapper;
using AutoMapper.QueryableExtensions;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Tags.Queries.GetTagById;

internal class GetTagByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetTagByIdQuery, Result<GetTagByIdQueryResponse>>
{
    public async Task<Result<GetTagByIdQueryResponse>> Handle(
        GetTagByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Tags
            .Where(t => t.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<GetTagByIdQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        return result;
    }
}
