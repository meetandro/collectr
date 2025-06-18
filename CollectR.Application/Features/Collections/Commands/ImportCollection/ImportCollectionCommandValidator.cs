using FluentValidation;

namespace CollectR.Application.Features.Collections.Commands.ImportCollection;

public sealed class ImportCollectionCommandValidator : AbstractValidator<ImportCollectionCommand>
{
    public ImportCollectionCommandValidator()
    {
        RuleFor(x => x.FileName).NotEmpty().WithMessage("File name is required.");
    }
}
