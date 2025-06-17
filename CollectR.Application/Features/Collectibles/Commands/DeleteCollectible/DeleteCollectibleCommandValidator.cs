using FluentValidation;

namespace CollectR.Application.Features.Collectibles.Commands.DeleteCollectible;

public sealed class DeleteCollectibleCommandValidator : AbstractValidator<DeleteCollectibleCommand>
{
    public DeleteCollectibleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
