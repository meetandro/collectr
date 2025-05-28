using CollectR.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CollectR.Application.Behaviors;

internal sealed class LoggingPipelineBehavior<TRequest, TResponse>(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation("Request sent: {RequestName}", requestName);

        try
        {
            TResponse result = await next();

            logger.LogInformation("Successfully handled request: {RequestName}", requestName);

            return result;
        }
        catch (EntityNotFoundException ex)
        {
            logger.LogError(ex, "Error handling request: {RequestName}, requested entity has not been found.", requestName); // details in exception
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled Exception for request: {RequestName}", requestName);
            throw;
        }
    }
}
