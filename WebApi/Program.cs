using System.Reflection.Metadata;
using Application.Interfaces.Services;
using Application.Services;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure;
using Application;
using WebApi.Configuration;
using Application.Interfaces.Infrastructure.Mongo;
using Infrastructure.Services.MongoDB.Adapters;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Comments in program
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterServices();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductAdapter>();
IWebHostEnvironment environment = builder.Environment;
IConfiguration configuration = builder.Configuration;

#region ProgramConfiguration
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    IConfigurationRoot configurationRoot = config
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment.ApplicationName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

});
#endregion ProgramConfiguration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterServices();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductAdapter>();
builder.Services.AddMongoDataBase(
    builder.Configuration.GetSection("AppSettings:ConnectionString").Value,
    builder.Configuration.GetSection("AppSettings:Database").Value,
    builder.Configuration.GetSection("AppSettings:CollectionName").Value
);

builder.Services.AddHealthChecks();

// Application configures
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

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