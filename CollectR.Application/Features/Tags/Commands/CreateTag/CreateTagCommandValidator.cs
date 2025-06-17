using FluentValidation;

namespace CollectR.Application.Features.Tags.Commands.CreateTag;

public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Hex)
            .NotEmpty()
            .WithMessage("Hex value is required.")
            .Matches("^#(?:[0-9a-fA-F]{6})$")
            .WithMessage("Hex must be a valid 6-digit hexadecimal.");

        RuleFor(x => x.CollectionId).NotEmpty().WithMessage("CollectionId is required.");
    }
}
