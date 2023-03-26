using Discount.Api.CustomMiddleware;

namespace Discount.Api.Middleware;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseException(this IApplicationBuilder applicationBuilder) =>
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();
}