using CollectR.Domain.Enums;
using FluentValidation;
using System.Text.Json;

namespace CollectR.Application.Features.Collectibles.Commands.CreateCollectible;

public sealed class CreateCollectibleCommandValidator : AbstractValidator<CreateCollectibleCommand>
{
    public CreateCollectibleCommandValidator()
    {
        RuleFor(x => x.Title) // format these well
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Currency)
            .MaximumLength(100).WithMessage("Custom currency cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Currency));

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0).When(x => x.Value.HasValue)
            .WithMessage("Value cannot be negative.");

        RuleFor(x => x.AcquiredDate)
            .Must(date => !date.HasValue || date.Value <= DateTime.UtcNow)
            .WithMessage("AcquiredDate cannot be in the future.");

        RuleFor(x => x.SortIndex)
            .InclusiveBetween(0, 1000)
            .WithMessage("SortIndex must be between 0 and 1000.");

        RuleFor(x => x.Color)
            .Must(color => !color.HasValue || Enum.IsDefined(typeof(Color), color.Value))
            .WithMessage("Color must be a valid value.");

        RuleFor(x => x.Condition)
            .Must(condition => !condition.HasValue || Enum.IsDefined(typeof(Condition), condition.Value))
            .WithMessage("Condition must be a valid value.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(x => x.CollectionId)
            .NotEmpty().WithMessage("CollectionId is required.");

        RuleFor(x => x.Metadata)
            .Must(BeValidKeyValueJson)
            .WithMessage("Metadata must be a valid JSON object containing only key-value pairs.");
    }

    private static bool BeValidKeyValueJson(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return true;

        try
        {
            using var doc = JsonDocument.Parse(value);

            if (doc.RootElement.ValueKind != JsonValueKind.Object)
                return false;

            foreach (var property in doc.RootElement.EnumerateObject())
            {
                var kind = property.Value.ValueKind;

                if (kind != JsonValueKind.String &&
                    kind != JsonValueKind.Number &&
                    kind != JsonValueKind.True &&
                    kind != JsonValueKind.False &&
                    kind != JsonValueKind.Null)
                {
                    return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
