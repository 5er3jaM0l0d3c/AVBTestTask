using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Application.Services;
using ProductService.Grpc.Service;
using ProductService.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
// Add services to the container.
var dbHost = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
    ? "db2"
    : "localhost";

// Строим строку подключения
var connectionString = $"Server={dbHost};Port=5432;Database=ProductServiceDB;User Id=postgres;Password=postgres;";

// Регистрируем контекст
builder.Services.AddDbContext<ProductServiceDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<ProductServerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductServiceDbContext>();

    try
    {
        // Проверяем, есть ли pending миграции
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            Console.WriteLine($"Applying migrations: {string.Join(", ", pendingMigrations)}");
            await dbContext.Database.MigrateAsync();
            Console.WriteLine("Migrations applied successfully");
        }
        else
        {
            Console.WriteLine("No pending migrations to apply");
        }
    }
    catch
    {
    }
}

app.MapGrpcService<ProductServerService>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
