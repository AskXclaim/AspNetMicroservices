namespace Catalog.Api.CustomMiddleware;

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        HttpStatusCode statusCode;
        ProblemDetails problemDetails;
        switch (exception)
        {
            case BadRequestException badRequestException:
            {
                statusCode = HttpStatusCode.BadRequest;
                problemDetails = GetProblemDetails(badRequestException, nameof(BadRequestException), statusCode);
                break;
            }
            case NotFoundException notFoundException:
            {
                statusCode = HttpStatusCode.NotFound;
                problemDetails = GetProblemDetails(notFoundException, nameof(NotFoundException), statusCode);
                break;
            }
            default:
            {
                statusCode = HttpStatusCode.InternalServerError;
                problemDetails = GetProblemDetails(exception, nameof(Exception), statusCode);
                break;
            }
        }

        _logger.LogError(problemDetails.Detail, problemDetails);
        context.Response.StatusCode = (int) statusCode;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private ProblemDetails GetProblemDetails(Exception exception, string type, HttpStatusCode statusCode)
    {
        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Type = type,
            Status = (int) statusCode,
            Detail = exception.InnerException == null
                ? exception.InnerException?.Message
                : exception.Message
        };

        return problemDetails;
    }
}