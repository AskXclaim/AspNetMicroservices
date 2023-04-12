using Discount.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.WebHost.ConfigureKestrel(options =>
    options.ListenLocalhost(6006, o => o.Protocols = HttpProtocols.Http2));

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();


var app = builder.Build();
app.UseException();
// Configure the HTTP request pipeline.
var connectionString = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
app.MigrateDatabase<Program>(connectionString);
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();