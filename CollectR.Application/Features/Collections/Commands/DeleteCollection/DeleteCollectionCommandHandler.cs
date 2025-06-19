using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

internal sealed class DeleteCollectionCommandHandler(
    ICollectionRepository collectionRepository,
    IFileService fileService
) : ICommandHandler<DeleteCollectionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteCollectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var collection = await collectionRepository.GetWithDetailsAsync(request.Id);

        if (collection is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        var images = collection.Collectibles.SelectMany(c => c.Images);

        foreach (var image in images)
        {
            fileService.DeleteFile(image.Uri);
        }

        await collectionRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
