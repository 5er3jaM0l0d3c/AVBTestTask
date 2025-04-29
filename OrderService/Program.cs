using OrderAPI.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Infrastructure.DbContexts;
using OrderAPI.Application.Services;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddGrpc();

var dbHost = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
    ? "db1"
    : "localhost";

// Строим строку подключения
var connectionString = $"Server={dbHost};Port=5432;Database=OrderServiceDB;User Id=postgres;Password=postgres;";

// Регистрируем контекст
builder.Services.AddDbContext<OrderServiceDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IOrder, OrderServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderServiceDbContext>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run("https://*:5056");
