const string ALllowAllCors = "AllowAllCors";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(opt => opt.AddPolicy(
    ALllowAllCors, corsPolicyBuilder =>
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddStackExchangeRedisCache(opt =>
    opt.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();

app.UseException();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();