namespace Discount.Api.CustomMiddleware;

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
        catch (Exception e)
        {
            Console.WriteLine(e);
            var problemDetail = new ProblemDetails()
            {
                Title = "Server Exception",
                Type =nameof(HttpStatusCode.InternalServerError),
                Detail = e.InnerException==null?e.Message:e.InnerException.Message,
                Status = (int)HttpStatusCode.InternalServerError
            };
            _logger.LogError(problemDetail.Detail);
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetail);
        }
    }
}