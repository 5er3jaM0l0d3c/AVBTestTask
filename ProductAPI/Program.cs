using Microsoft.EntityFrameworkCore;
using ProductAPI.Service;
using ProductEntities;
using ProductServices.Interface;
using ProductServices.Service;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
// Add services to the container.
var dbHost = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
    ? "db2"
    : "localhost";

// ������ ������ �����������
var connectionString = $"Server={dbHost};Port=5432;Database=ProductServiceDB;User Id=postgres;Password=postgres;";

// ������������ ��������
builder.Services.AddDbContext<ProductServiceDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IProduct, ProductServices.Service.ProductServices>();

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
        // ���������, ���� �� pending ��������
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

app.MapGrpcService<ProductService>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run("http://*:81");
