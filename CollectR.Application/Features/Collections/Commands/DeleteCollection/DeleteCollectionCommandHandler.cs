using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

internal sealed class DeleteCollectionCommandHandler(ICollectionRepository collectionRepository)
    : ICommandHandler<DeleteCollectionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var collection = await collectionRepository.GetByIdAsync(request.Id);

        if (collection is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        await collectionRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
