using FluentValidation;

namespace CollectR.Application.Features.Collectibles.Commands.UpdateCollectibleTags;

public sealed class UpdateCollectibleTagsCommandValidator : AbstractValidator<UpdateCollectibleTagsCommand>
{
    public UpdateCollectibleTagsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Collectible Id is required.");

        RuleFor(x => x.TagIds)
            .NotNull().WithMessage("TagIds collection is required.")
            .Must(tagIds => tagIds.All(id => id != Guid.Empty))
            .WithMessage("TagIds cannot contain empty GUIDs.");
    }
}
