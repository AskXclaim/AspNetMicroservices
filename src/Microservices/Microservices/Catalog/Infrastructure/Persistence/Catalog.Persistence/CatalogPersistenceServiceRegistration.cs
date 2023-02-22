namespace Catalog.Persistence;

public static class CatalogPersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(IServiceCollection services, IConfiguration configuration )
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
        services.AddScoped<ICatalogContext, CatalogContext>();
        return services;
    }
}