using System.Reflection.Metadata;
using Application.Interfaces.Common;
using Application.Interfaces.Services;
using Application.Services;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Comentario en progrmam
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddSingleton<IHandle>();
IWebHostEnvironment environment = builder.Environment;
IConfiguration configuration = builder.Configuration;

#region ProgramConfiguration
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    IConfigurationRoot configurationRoot = config
        .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment.ApplicationName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

});
#endregion ProgramConfiguration
builder.Services.AddControllers();
string MongoConnectionSecret = builder.Configuration.GetValue<string>(builder.Configuration.GetSection("Secrets:MongoConnection").Value);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMongoDataBase(MongoConnectionSecret, builder.Configuration.GetSection("AppSettings:Database").Value, builder.Configuration.GetSection("AppSettings:CollectionName").Value);

builder.Services.AddHealthChecks();

// Configuraciones de aplicación
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