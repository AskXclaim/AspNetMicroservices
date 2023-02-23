namespace Catalog.Application;

public static class CatalogApplicationServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}