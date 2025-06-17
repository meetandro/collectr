using FluentValidation;

namespace CollectR.Application.Features.Tags.Commands.UpdateTag;

public sealed class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");

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
    }
}
