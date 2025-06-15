using AutoMapper;
using CollectR.Application.Abstractions;
using CollectR.Application.Abstractions.Messaging;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectible;

internal sealed class UpdateCollectibleCommandHandler(
    ICollectibleRepository collectibleRepository,
    IApplicationDbContext context,
    IMapper mapper
) : ICommandHandler<UpdateCollectibleCommand, Result<UpdateCollectibleCommandResponse>>
{
    public async Task<Result<UpdateCollectibleCommandResponse>> Handle(
        UpdateCollectibleCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = await context
            .Collectibles.Include(c => c.CollectibleTags)
            .Include(c => c.Images)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (collectible is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        mapper.Map(request, collectible);

        HandleTags(request, collectible);
        await HandleImagesAsync(request, collectible, cancellationToken);

        var result = collectibleRepository.Update(collectible);

        return mapper.Map<UpdateCollectibleCommandResponse>(result);
    }

    private void HandleTags(UpdateCollectibleCommand request, Collectible collectible)
    {
        // Your custom tag logic goes here.
        // Example: collectible.CollectibleTags = await _yourTagService.UpdateTagsAsync(...);
    }

    private async Task HandleImagesAsync(
        UpdateCollectibleCommand request,
        Collectible collectible,
        CancellationToken cancellationToken
    )
    {
        // Your custom image handling logic goes here.
        // Example: collectible.Images = await _yourImageService.ProcessImagesAsync(...);
    }
}
