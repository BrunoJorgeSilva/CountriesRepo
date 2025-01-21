using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CountriesApi.Application.Interfaces;
using CountriesApi.Infrastructure.Repositories;
using CountriesApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CountriesApi.Infrastructure.External;

namespace CountriesApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryIntegration, CountryIntegration>();
            
            return services;
        }
    }
}