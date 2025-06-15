using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CollectR.Application.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var validationFailures = await ValidateAsync(request);

        if (validationFailures.Length != 0)
        {
            throw new Exceptions.ValidationException(validationFailures);
        }

        return await next();
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
    {
        if (!validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context))
        );

        var validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return validationFailures;
    }
}
