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
            var exceptionMessage = GetExceptionMessage(exception);

            _logger.LogError(exceptionMessage);

            var problem = GetProblemDetails(exceptionMessage);
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    private string GetExceptionMessage(Exception exception) =>
        string.IsNullOrWhiteSpace(exception.InnerException?.Message)
            ? exception.Message
            : exception.InnerException.Message;

    private ProblemDetails GetProblemDetails(string exceptionMessage) => new()
    {
        Title = "Server error",
        Type = nameof(HttpStatusCode.InternalServerError),
        Status = (int) HttpStatusCode.InternalServerError,
        Detail = exceptionMessage
    };
}