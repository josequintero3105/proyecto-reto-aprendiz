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
using Application.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Helpers.Handle;
using Application.Interfaces.Infrastructure.Commands;
using Application.Common.Helpers.Commands;
using Application.Common.FluentValidations.Extentions;
using System.Reflection.PortableExecutable;
using Application.DTOs.ApiEntities.Response;
using Application.DTOs.ApiEntities.Output;
using Core.Enumerations;
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
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceAdapter>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartAdapter>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerAdapter>();
builder.Services.AddHttpClientServices();
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
builder.Services.UseRestApiFilters();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductAdapter>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceAdapter>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartAdapter>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerAdapter>();
builder.Services.AddHttpClientServices();
builder.Services.AddHttpClient("Pasarela", client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:BaseUrl").Value!);
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "75d9e533382a4f05855558d6216919a7");
    client.DefaultRequestHeaders.Add("ApplicationKey", "6099675acae5400001387631");
    client.DefaultRequestHeaders.Add("ApplicationToken", "609966e0a39cd000012cc490");
    client.DefaultRequestHeaders.Add("SCLocation", "0,0");
    client.DefaultRequestHeaders.Add("SCOrigen", "Staging");
    client.DefaultRequestHeaders.Add("country", "co");
    //client.DefaultRequestHeaders.Add("Cookie", "__cf_bm=EqZ.c_ccNeK5MO9yB1DnT3NOxDvg6Hwc5Bv3O5z2QdY-1720470996-1.0.1.1-sLHGLmOgTDC59EjsFlNS0Q6BNcC2rctfSMPPwakOUNF5N691gxAfc9IGfhtZV1CuaV6eqMBEGWAsi_O2yUzJjA");
});
builder.Services.AddScoped<IHandle, Application.Common.Helpers.Handle.Handle>();
builder.Services.AddMongoDataBase(
    builder.Configuration.GetSection("AppSettings:ConnectionString").Value!,
    builder.Configuration.GetSection("AppSettings:Database").Value!
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