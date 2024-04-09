using System.Net.NetworkInformation;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Services.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddHttpClientServices();
            return services;
        }
        public static IServiceCollection AddMongoDataBase(this IServiceCollection services, string mongoConnectionString, string dataBaseName, string collectionName)
        {
            services.AddSingleton<IContext>(provider => new DataBaseContext(mongoConnectionString, $"{dataBaseName}", collectionName));
            return services;
        }
    }
}
