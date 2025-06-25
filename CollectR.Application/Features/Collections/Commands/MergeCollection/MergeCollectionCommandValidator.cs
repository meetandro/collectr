using FluentValidation;

namespace CollectR.Application.Features.Collections.Commands.MergeCollection;

public sealed class MergeCollectionCommandValidator : AbstractValidator<MergeCollectionCommand>
{
    public MergeCollectionCommandValidator()
    {
        RuleFor(x => x.File.FileName).NotEmpty().WithMessage("File name is required.");
    }
}

