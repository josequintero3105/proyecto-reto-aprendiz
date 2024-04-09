using System.Net.NetworkInformation;
using Application.Interfaces.Services;
using Application.Services;
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
    }
}
