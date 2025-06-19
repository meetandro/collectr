using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;

namespace CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;

internal sealed class DeleteCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
    IFileService fileService
) : ICommandHandler<DeleteCollectibleCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteCollectibleCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = await collectibleRepository.GetWithDetailsAsync(request.Id);

        if (collectible is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        var images = collectible.Images;

        foreach (var image in images)
        {
            fileService.DeleteFile(image.Uri, "images");
        }

        await collectibleRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
