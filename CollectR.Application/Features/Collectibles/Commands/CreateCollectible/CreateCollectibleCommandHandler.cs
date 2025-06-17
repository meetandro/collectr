using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;

namespace CollectR.Application.Features.Collectibles.Commands.CreateCollectible;

internal sealed class CreateCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
    IImageRepository imageRepository,
    IFileService fileService,
    IMapper mapper
) : ICommandHandler<CreateCollectibleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateCollectibleCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = mapper.Map<Collectible>(request);

        var result = await collectibleRepository.CreateAsync(collectible);

        if (request.Images is not null && request.Images.Any())
        {
            var images = new List<Image>();

            foreach (var image in request.Images)
            {
                var savedFileName = await fileService.SaveFileInFolderAsync(image, "images");
                var imageUri = $"/images/{savedFileName}";

                images.Add(new Image { Uri = imageUri, Collectible = collectible });
            }

            await imageRepository.CreateRangeAsync(images);
        }

        return result.Id;
    }
}
