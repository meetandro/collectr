using CollectR.Application.Abstractions;
using CollectR.Application.Contracts.Persistence;
using MediatR;

namespace CollectR.Application.Behaviors;

internal sealed class UnitOfWorkPipelineBehavior<TRequest, TResponse>(
    IApplicationDbContext context
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (IsQuery())
        {
            return await next();
        }

        var response = await next();

        await context.SaveChangesAsync(cancellationToken);

        return response;
    }

    private static bool IsQuery()
    {
        return typeof(IQuery<TResponse>).IsAssignableFrom(typeof(TRequest));
    }
}
