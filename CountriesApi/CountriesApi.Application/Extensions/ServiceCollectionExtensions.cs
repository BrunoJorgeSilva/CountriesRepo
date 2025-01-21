using CountriesApi.Application.Interfaces;
using CountriesApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CountriesApi.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            return services;
        }
    }
}