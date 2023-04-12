namespace Discount.Shared.CustomMiddleware;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseException(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();
}