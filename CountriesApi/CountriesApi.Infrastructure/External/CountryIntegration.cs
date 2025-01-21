using CountriesApi.Application.Interfaces;
using CountriesApi.Domain.Entities;
using CountriesApi.Domain.ResponseObjects;
using CountriesApi.Domain.ResponseObjects.DTOs;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CountriesApi.Infrastructure.External
{
    public class CountryIntegration : ICountryIntegration
    {
        private readonly ILogger<CountryIntegration> _logger;
        private readonly string urlCountriesPublicApi = Environment.GetEnvironmentVariable("URLCOUNTRIESPUBLICAPI") ?? string.Empty;

        public CountryIntegration(ILogger<CountryIntegration> logger)
        {
            _logger = logger;
        }
        public async Task<List<CountryDto>> GetCountries()
        {
            HttpClient httpClient = new HttpClient();
            _logger.LogInformation("[CountryIntegration.GetCountries] Starting Integration with public api. Url:{0}", urlCountriesPublicApi);
            try
            {
                var response = await httpClient.GetFromJsonAsync<ApiResponse>(urlCountriesPublicApi);
                _logger.LogInformation("[CountryIntegration.GetCountries] Response: {0}", response);
                if (response != null && response.StatusCode == 200 && response.Data != null)
                {
                    return response.Data
                        .DistinctBy(c => c.Name)  
                        .ToList();
                }
                return new List<CountryDto>();

            }
            catch (Exception ex)
            {
                _logger.LogError("[CountryIntegration.GetCountries] Error Integrating with {0}, ErrorMessage: {1}", urlCountriesPublicApi, ex.Message);
                return new List<CountryDto>();
            }
        }
    }
}
