using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;
using MediatR;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;

internal class UpdateCollectibleCommandHandler(ICollectibleRepository collectibleRepository, IImageRepository imageRepository, IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper) : IRequestHandler<UpdateCollectibleCommand, UpdateCollectibleCommandResponse>
{
    public async Task<UpdateCollectibleCommandResponse> Handle(UpdateCollectibleCommand request, CancellationToken cancellationToken)
    {
        var collectible = await collectibleRepository.GetByIdAsync(request.Id)
            ?? throw new NotImplementedException();

        mapper.Map(request, collectible);

        if (collectible.Images is not null && collectible.Images.Count > 0)
        {
            foreach (var file in collectible.Images)
            {
                if (request.ExistingImages is not null && !request.ExistingImages.Contains(file.Uri))
                {
                    fileService.DeleteFileInFolder(file.Uri, "images");
                    await imageRepository.DeleteAsync(file.Id);
                }
            }
        }

        var imageUris = new List<Image>();

        if (request.NewImages is not null && request.NewImages.Any())
        {
            foreach (var file in request.NewImages)
            {
                var savedFileName = await fileService.SaveFileInFolderAsync(file, "images");
                var imageUrl = $"/images/{savedFileName}";

                imageUris.Add(
                    new Image
                    {
                        Uri = imageUrl,
                        CollectibleId = collectible.Id,
                    }
                );
            }

            await imageRepository.CreateRangeAsync(imageUris);
        }

        foreach (var imageUrl in imageUris)
        {
            collectible.Images.Add(imageUrl);
        }

        var result = collectibleRepository.Update(collectible);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UpdateCollectibleCommandResponse>(result);
    }
}
