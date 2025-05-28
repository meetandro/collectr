using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Collectibles.Queries.GetAllCollectibles;

internal class GetAllCollectiblesQueryHandler(ICollectibleRepository collectibleRepository, IMapper mapper) : IRequestHandler<GetAllCollectiblesQuery, IEnumerable<GetAllCollectiblesQueryResponse>>
{
    public async Task<IEnumerable<GetAllCollectiblesQueryResponse>> Handle(GetAllCollectiblesQuery request, CancellationToken cancellationToken)
    {
        var result = await collectibleRepository.GetAllAsync();

        return result.Select(mapper.Map<GetAllCollectiblesQueryResponse>);
    }
}
