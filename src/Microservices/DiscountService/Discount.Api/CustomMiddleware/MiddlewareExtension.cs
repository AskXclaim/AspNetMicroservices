namespace Discount.Api.CustomMiddleware;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseException(this IApplicationBuilder applicationBuilder) =>
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();
}