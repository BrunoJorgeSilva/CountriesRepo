using CountriesApi.Domain.ResponseObjects.DTOs;

namespace CountriesApi.Application.Interfaces
{
    public interface ICountryIntegration
    {
        Task<List<CountryDto>> GetCountries();
    }
}
