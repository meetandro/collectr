using FluentValidation;

namespace CollectR.Application.Features.Collections.Commands.DeleteCollection;

public sealed class DeleteCollectionCommandValidator : AbstractValidator<DeleteCollectionCommand>
{
    public DeleteCollectionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
