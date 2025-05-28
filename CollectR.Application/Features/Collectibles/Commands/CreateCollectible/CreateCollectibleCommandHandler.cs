using AutoMapper;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;
using MediatR;

namespace CollectR.Application.Features.Collectibles.Commands.CreateCollectible;

internal class CreateCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IMapper mapper
) : IRequestHandler<CreateCollectibleCommand, int>
{
    public async Task<int> Handle(
        CreateCollectibleCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = mapper.Map<Collectible>(request);

        var result = await collectibleRepository.CreateAsync(collectible);

        if (request.Images is not null && request.Images.Any())
        {
            var images = new List<Image>();

            foreach (var image in request.Images) // try to remove foreach
            {
                var savedFileName = await fileService.SaveFileInFolderAsync(image, "images");
                var imageUri = $"/images/{savedFileName}";

                images.Add(
                    new Image
                    {
                        Uri = imageUri,
                        Collectible = collectible
                    }
                );
            }

            await imageRepository.CreateRangeAsync(images);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Id;
    }
}
