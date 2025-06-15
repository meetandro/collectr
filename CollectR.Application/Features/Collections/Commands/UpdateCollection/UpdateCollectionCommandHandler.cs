using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Collections.Commands.UpdateCollection;

internal sealed class UpdateCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    IMapper mapper
) : ICommandHandler<UpdateCollectionCommand, Result<UpdateCollectionCommandResponse>>
{
    public async Task<Result<UpdateCollectionCommandResponse>> Handle(
        UpdateCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var collection = await collectionRepository.GetByIdAsync(request.Id);

        if (collection is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        mapper.Map(request, collection);

        var result = mapper.Map<UpdateCollectionCommandResponse>(
            collectionRepository.Update(collection)
        );

        return result;
    }
}
