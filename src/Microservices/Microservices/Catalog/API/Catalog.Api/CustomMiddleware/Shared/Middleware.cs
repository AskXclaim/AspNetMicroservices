namespace Catalog.Api.CustomMiddleware.Shared;

public static class Middleware
{
    public static IApplicationBuilder UseException(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();
}