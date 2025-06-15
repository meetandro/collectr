using FluentValidation;

namespace CollectR.Application.Features.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().WithMessage("Category name can't be empty.");
    }
}
