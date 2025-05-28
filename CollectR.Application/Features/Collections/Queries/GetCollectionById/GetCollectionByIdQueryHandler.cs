using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Features.Collections.Queries.GetCollectionById;

internal class GetCollectionByIdQueryHandler(ICollectionRepository collectionRepository, IMapper mapper) : IRequestHandler<GetCollectionByIdQuery, GetCollectionByIdQueryResponse>
{
    public async Task<GetCollectionByIdQueryResponse> Handle(GetCollectionByIdQuery request, CancellationToken cancellationToken)
    {
        var collection = await collectionRepository.GetByIdAsync(request.Id)
            ?? throw new NotImplementedException();

        var result = mapper.Map<GetCollectionByIdQueryResponse>(collection);

        return result;
    }
}
