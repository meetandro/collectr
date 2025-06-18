using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Common.Errors;
using CollectR.Application.Common.Result;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectibleTags;

internal sealed class UpdateCollectibleTagsCommandHandler(
    ICollectibleRepository collectibleRepository,
    ITagRepository tagRepository
) : ICommandHandler<UpdateCollectibleTagsCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        UpdateCollectibleTagsCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = await collectibleRepository.GetWithDetailsAsync(request.Id);

        if (collectible is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        var tags = await tagRepository.GetByIdsAsync(request.TagIds);

        if (tags.Count() != request.TagIds.Count())
        {
            return EntityErrors.OneOrMoreDoesNotExist();
        }

        collectible.CollectibleTags.Clear();

        foreach (var tag in tags)
        {
            collectible.CollectibleTags.Add(new CollectibleTag
            {
                CollectibleId = collectible.Id,
                TagId = tag.Id,
            });
        }

        return Result.Success();
    }
}
