using Microsoft.EntityFrameworkCore;
using ProductAPI.Service;
using ProductEntities;
using ProductServices.Interface;
using ProductServices.Service;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
// Add services to the container.
builder.Services.AddDbContext<ProductServiceDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProduct, ProductServices.Service.ProductServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductServiceDbContext>();
    await dbContext.Database.MigrateAsync();
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
