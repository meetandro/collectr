using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;

namespace CollectR.Application.Features.Collectibles.Commands.CreateCollectible;

internal sealed class CreateCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
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
            foreach (var image in request.Images)
            {
                var savedFileName = await fileService.SaveFileInFolderAsync(image, "images");
                var imageUri = $"/images/{savedFileName}";

                collectible.Images.Add(new Image { Uri = imageUri, Collectible = collectible });
            }
        }

        return result.Id;
    }
}
