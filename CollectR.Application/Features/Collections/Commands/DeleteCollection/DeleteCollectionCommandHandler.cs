using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

internal sealed class DeleteCollectionCommandHandler(ICollectionRepository collectionRepository)
    : ICommandHandler<DeleteCollectionCommand, Result>
{
    public async Task<Result> Handle(
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
