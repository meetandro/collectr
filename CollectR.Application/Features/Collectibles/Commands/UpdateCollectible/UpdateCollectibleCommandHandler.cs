using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;
//TODO : Further testing requied, what if i only assign id? will i be able to break free from the image repo?
internal sealed class UpdateCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
    IImageRepository imageRepository,
    IFileService fileService,
    IMapper mapper
) : ICommandHandler<UpdateCollectibleCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        UpdateCollectibleCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = await collectibleRepository.GetWithDetailsAsync(request.Id);

        if (collectible is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        mapper.Map(request, collectible);

        string[] uris = request.ExistingImageUris.Split(',');

        if (
            collectible.Images is not null
            && collectible.Images.Count > 0
            && request.ExistingImageUris is not null
            && uris.Length != 0
        )
        {
            foreach (var image in collectible.Images)
            {
                if (!request.ExistingImageUris.Contains(image.Uri))
                {
                    fileService.DeleteFile(image.Uri, "images");
                    await imageRepository.DeleteAsync(image.Id);
                }
            }
        }

        if (request.NewImages is not null && request.NewImages.Any())
        {
            foreach (var file in request.NewImages)
            {
                var savedFileName = await fileService.SaveFileInFolderAsync(file, "images");
                var imageUrl = $"/images/{savedFileName}";

                await imageRepository.CreateAsync(
                    new Image { Uri = imageUrl, CollectibleId = collectible.Id }
                );
            }
        }

        collectibleRepository.Update(collectible);

        return Result.Success();
    }
}
