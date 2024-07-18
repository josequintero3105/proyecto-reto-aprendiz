using System.Net.NetworkInformation;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Infrastructure.RestService;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Services.MongoDB;
using Infrastructure.Services.MongoDB.Adapters;
using Infrastructure.Services.Rest;
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

        //public static IServiceCollection AddApiPasarela(this IServiceCollection services, 
        //    string url, string subscription, string key, string token, string location, string origin, string country, string cookie)
        //{
        //    services.AddSingleton<IApiContext>(provider => new ApiContext(url, subscription, key, token, location, origin, country, cookie));
        //    return services;
        //}

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICreateRepository, CreateAdapter>();
            services.AddScoped<IGetRepository, GetAdapter>();
            services.AddScoped<ITokenRepository, TokenAdapter>();
            return services;
        }
    }
}
