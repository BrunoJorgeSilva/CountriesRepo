using CountriesApi.Application.Common;
using CountriesApi.Domain.Entities;

namespace CountriesApi.Application.Interfaces
{
    public interface ICountryService
    {
        Task<Result<List<Country>>> GetAllCountries();
        Task<Result<bool>> UpdateCountries();
    }
}
