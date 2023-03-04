using System.Net;

namespace Basket.Api.CustomMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            var exceptionMessage = string.IsNullOrWhiteSpace(exception?.InnerException?.Message)
                ? exception.Message
                : exception.InnerException.Message;
            _logger.LogError(exceptionMessage);
            var problem = new ProblemDetails()
            {
                Title = "Server error",
                Type = nameof(HttpStatusCode.InternalServerError),
                Status = (int) HttpStatusCode.InternalServerError,
                Detail = exceptionMessage
            };
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}