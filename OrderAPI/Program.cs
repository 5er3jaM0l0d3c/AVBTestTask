using Microsoft.EntityFrameworkCore;
using OrderEntities;
using OrderServices.Interface;
using OrderServices.Service;
using ProductEntities;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbHost = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
    ? "db1"
    : "localhost";

// Строим строку подключения
var connectionString = $"Server={dbHost};Port=5432;Database=OrderServiceDB;User Id=postgres;Password=postgres;";

// Регистрируем контекст
builder.Services.AddDbContext<OrderServiceDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IOrder, OrderServices.Service.OrderServices>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run("http://*:82");
