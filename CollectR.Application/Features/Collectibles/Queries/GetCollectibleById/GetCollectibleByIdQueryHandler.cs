using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Collectibles.Queries.GetCollectibleById;

internal class GetCollectibleByIdQueryHandler(ICollectibleRepository collectibleRepository, IMapper mapper) : IRequestHandler<GetCollectibleByIdQuery, GetCollectibleByIdQueryResponse>
{
    public async Task<GetCollectibleByIdQueryResponse> Handle(GetCollectibleByIdQuery request, CancellationToken cancellationToken)
    {
        var collectible = await collectibleRepository.GetByIdAsync(request.Id);

        return mapper.Map<GetCollectibleByIdQueryResponse>(collectible);
    }
}
