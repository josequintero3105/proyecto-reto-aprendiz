using System.Net.NetworkInformation;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Services.MongoDB;
using Infrastructure.Services.MongoDB.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddMongoDataBase(this IServiceCollection services, string mongoConnectionString, string dataBaseName, string collectionName)
        {
            services.AddSingleton<IContext>(provider => new DataBaseContext(mongoConnectionString, $"{dataBaseName}", collectionName));
            return services;
        }
    }
}
