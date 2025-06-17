using CollectR.Application.Abstractions;
using CollectR.Application.Common;
using CollectR.Application.Contracts.Persistence;
using CollectR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectibleTags;

internal sealed class UpdateCollectibleTagsCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateCollectibleTagsCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        UpdateCollectibleTagsCommand request,
        CancellationToken cancellationToken
    )
    {
        var collectible = await context
            .Collectibles.Include(c => c.CollectibleTags)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (collectible is null)
        {
            return EntityErrors.NotFound(request.Id);
        }

        var tags = await context
            .Tags.Where(t => request.TagIds.Contains(t.Id))
            .ToListAsync(cancellationToken);
        if (tags.Count != request.TagIds.Count())
        {
            return EntityErrors.OneOrMoreDoesntExist();
        }

        collectible.CollectibleTags.Clear();

        collectible.CollectibleTags = tags.Select(tag => new CollectibleTag
            {
                CollectibleId = collectible.Id,
                TagId = tag.Id,
            })
            .ToList();

        return Result.Success();
    }
}
