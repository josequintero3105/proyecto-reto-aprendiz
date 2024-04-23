using System.Text.Json;
using Application;
using Application.Common.Utilities;
using Application.DTOs;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Filters;
using RestApi.Filters;

namespace WebApi.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

        public static IConfigurationBuilder AddJsonProvider(this IConfigurationBuilder configuration)
        {
            configuration.AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true);
            return configuration;
        }

        public static IServiceCollection UseRestApiFilters(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<SuccessFilter>();
            });
            return services;
        }
    }
}
