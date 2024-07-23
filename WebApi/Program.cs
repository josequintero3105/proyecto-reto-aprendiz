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
using Application.Common.Helpers.Handle;
using Application.Interfaces.Infrastructure.Commands;
using Application.Common.Helpers.Commands;
using Application.Common.FluentValidations.Extentions;
using System.Reflection.PortableExecutable;
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
builder.Services.AddScoped<ITransactionRepository, TransactionAdapter>();
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
builder.Services.AddScoped<ITransactionRepository, TransactionAdapter>();
builder.Services.AddHttpClientServices();
builder.Services.AddHttpClient("Pasarela", client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:BaseUrl").Value!);
    client.DefaultRequestHeaders.Add("Authorization", "Bearer ");
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b0dc8eb7924540e1913ab262b8500721");
    client.DefaultRequestHeaders.Add("ApplicationKey", "5d9b85f784c9d000019a9bff");
    client.DefaultRequestHeaders.Add("ApplicationToken", "5d9b6bd284c9d000019a9bfd");
    client.DefaultRequestHeaders.Add("SCLocation", "0,0");
    client.DefaultRequestHeaders.Add("SCOrigen", "Qa");
    client.DefaultRequestHeaders.Add("country", "co");
    client.DefaultRequestHeaders.Add("Cookie", "__cf_bm=EqZ.c_ccNeK5MO9yB1DnT3NOxDvg6Hwc5Bv3O5z2QdY-1720470996-1.0.1.1-sLHGLmOgTDC59EjsFlNS0Q6BNcC2rctfSMPPwakOUNF5N691gxAfc9IGfhtZV1CuaV6eqMBEGWAsi_O2yUzJjA");
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