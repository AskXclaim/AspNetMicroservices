namespace Discount.Shared.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, string connectionString, int? retry = 0)
    {
        var retryForAvailability = retry ?? 0;

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation(retryForAvailability == 0
                ? "Beginning to migrate postgresql"
                : $"Attempt{retryForAvailability}: to migrate postgresql");

            // var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            using var connection = new NpgsqlConnection(connectionString);

            logger.LogInformation(connectionString);

            connection.Open();
            using var command = new NpgsqlCommand {Connection = connection};
            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE Coupon(" +
                                  "Id SERIAL PRIMARY KEY NOT NULL, " +
                                  "ProductName VARCHAR(24) NOT NULL," +
                                  "Description TEXT," +
                                  "Amount DECIMAL(8,3)" +
                                  ")";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount)" +
                                  "VALUES ('IPhone X', 'IPhone Discount', 150)," +
                                  "('Samsung 10', 'Samsung Discount', 100)";

            command.ExecuteNonQuery();

            logger.LogInformation("Migrated Postgresql database.");
        }
        catch (NpgsqlException npgsqlException)
        {
            logger.LogError(npgsqlException,
                "An error occurred while migrating postgresql database, retry will be attempted");

            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host,connectionString, retryForAvailability);
            }
        }

        return host;
    }
}