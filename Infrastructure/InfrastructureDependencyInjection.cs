using System.Net.NetworkInformation;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Infrastructure.RestService;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Services.MongoDB;
using Infrastructure.Services.MongoDB.Adapters;
using Infrastructure.Services.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddMongoDataBase(this IServiceCollection services, string mongoConnectionString, string dataBaseName)
        {
            services.AddSingleton<IContext>(provider => new DataBaseContext(mongoConnectionString, $"{dataBaseName}"));
            return services;
        }

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICreateRepository, CreateAdapter>();
            services.AddScoped<IGetRepository, GetAdapter>();
            return services;
        }
    }
}
