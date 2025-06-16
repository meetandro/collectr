using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Collections.Commands.UpdateCollection;

internal sealed class UpdateCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    IMapper mapper
) : ICommandHandler<UpdateCollectionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
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

        collectionRepository.Update(collection);

        return Result.Success();
    }
}
