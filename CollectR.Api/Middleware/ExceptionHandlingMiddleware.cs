using System.Net;
using System.Text.Json;
using CollectR.Application.Exceptions;

namespace CollectR.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger,
    IHostEnvironment env
)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly IHostEnvironment _env = env;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        object errorMessage;

        switch (ex)
        {
            case ValidationException e:
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = e.Errors;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorMessage = "An error occurred.";
                break;
        }

        _logger.LogError(ex, "Error: {ErrorMessage}", errorMessage);

        var errorResponse = new
        {
            error = errorMessage,
            details = _env.IsDevelopment() ? ex.Message : null,
        };

        var result = JsonSerializer.Serialize(errorResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(result);
    }
}
