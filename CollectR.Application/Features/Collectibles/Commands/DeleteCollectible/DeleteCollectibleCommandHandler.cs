using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;

namespace CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;

internal sealed class DeleteCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
    IFileService fileService
) : ICommandHandler<DeleteCollectibleCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCollectibleCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = await collectibleRepository.GetByIdAsync(request.Id);

        if (collectible is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        var imageUris = collectible.Images.Select(i => i.Uri);

        foreach (var imageUri in imageUris)
        {
            fileService.DeleteFileInFolder(imageUri, "images");
        }

        await collectibleRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
