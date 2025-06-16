using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;

internal sealed class UpdateCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
    IApplicationDbContext context,
    IFileService fileService,
    IImageRepository imageRepository,
    IMapper mapper
) : ICommandHandler<UpdateCollectibleCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        UpdateCollectibleCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = await context.Collectibles
            .Include(c => c.CollectibleTags)
            .Include(c => c.Images)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (collectible is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        mapper.Map(request, collectible);

        await DeleteExistingImagesIfRequestDoesntContainThem(fileService, imageRepository, request, collectible);

        await CreateNewImagesIfRequestContainsThem(fileService, imageRepository, request, collectible);

        collectibleRepository.Update(collectible);

        return Result.Success();
    }

    private static async Task<List<Image>> CreateNewImagesIfRequestContainsThem(IFileService fileService, IImageRepository imageRepository, UpdateCollectibleCommand request, Collectible? collectible)
    {
        var imageUrls = new List<Image>();

        if (request.NewImages is not null && request.NewImages.Any())
        {
            foreach (var file in request.NewImages)
            {
                var savedFileName = await fileService.SaveFileInFolderAsync(file, "images");
                var imageUrl = $"/images/{savedFileName}";

                imageUrls.Add(
                    new Image
                    {
                        Uri = imageUrl,
                        CollectibleId = collectible.Id,
                    }
                );
            }

            await imageRepository.CreateRangeAsync(imageUrls); // test createdAt props
        }

        return imageUrls;
    }

    private static async Task DeleteExistingImagesIfRequestDoesntContainThem(IFileService fileService, IImageRepository imageRepository, UpdateCollectibleCommand request, Collectible? collectible)
    {
        string[] trueUris = request.ExistingImageUris.Split(',');

        if (collectible.Images is not null && collectible.Images.Count > 0 && request.ExistingImageUris is not null && trueUris.Length != 0)
        {
            foreach (var file in collectible.Images)
            {
                if (!request.ExistingImageUris.Contains(file.Uri))
                {
                    fileService.DeleteFileInFolder(file.Uri, "images");
                    await imageRepository.HardDeleteAsync(file.Id); // config
                }
            }
        }
    }
}
