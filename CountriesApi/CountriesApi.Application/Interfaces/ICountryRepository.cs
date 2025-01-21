using CountriesApi.Domain.Entities;

namespace CountriesApi.Application.Interfaces
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllAsync();
        Task<bool> UpdateAsync(List<Country> countries);    
        Task<bool> InsertAsync(List<Country> countries);
    }
}
