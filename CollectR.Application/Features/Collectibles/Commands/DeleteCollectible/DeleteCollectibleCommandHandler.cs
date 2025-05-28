using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using MediatR;

namespace CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;

internal class DeleteCollectibleCommandHandler(ICollectibleRepository collectibleRepository, IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<DeleteCollectibleCommand, bool>
{
    public async Task<bool> Handle(DeleteCollectibleCommand request, CancellationToken cancellationToken)
    {
        var collectible = await collectibleRepository.GetByIdAsync(request.Id)
            ?? throw new NotImplementedException();

        var imageUris = collectible.Images.Select(i => i.Uri);

        foreach (var imageUri in imageUris)
        {
            fileService.DeleteFileInFolder(imageUri, "images");
        }

        var result = await collectibleRepository.DeleteAsync(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
