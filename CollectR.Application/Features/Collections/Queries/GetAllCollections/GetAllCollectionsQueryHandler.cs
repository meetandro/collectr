using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Collections.Queries.GetAllCollections;

internal class GetAllCollectionsQueryHandler(ICollectionRepository collectionRepository, IMapper mapper) : IRequestHandler<GetAllCollectionsQuery, IEnumerable<GetAllCollectionsQueryResponse>>
{
    public async Task<IEnumerable<GetAllCollectionsQueryResponse>> Handle(GetAllCollectionsQuery request, CancellationToken cancellationToken)
    {
        var result = await collectionRepository.GetAllAsync();

        return result.Select(mapper.Map<GetAllCollectionsQueryResponse>);
    }
}
