using CountriesApi.Domain.Entities;
using CountriesApi.Infrastructure.Data;
using CountriesApi.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CountriesApi.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(ApplicationDbContext context, ILogger<CountryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Set<Country>().ToListAsync();
        }

        public async Task<bool> UpdateAsyncOld(List<Country> countries)
        {
            try
            {
                _context.Set<Country>().AddRange(countries);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[CountryRepository.UpdateAsync] Error: {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(List<Country> countries)
        {
            try
            {
                foreach (var country in countries)
                {
                    // Localiza o país no banco pelo Name
                    var existingCountry = await _context.Set<Country>().FirstOrDefaultAsync(c => c.Name == country.Name);
                    if (existingCountry != null)
                    {
                        existingCountry.Capital = country.Capital;
                        existingCountry.Region = country.Region;
                        existingCountry.SubRegion = country.SubRegion;
                        existingCountry.Population = country.Population;
                        existingCountry.Latitude = country.Latitude;
                        existingCountry.Longitude = country.Longitude;
                        existingCountry.Borders = country.Borders;
                        existingCountry.Timezones = country.Timezones;
                        existingCountry.Currency = country.Currency;
                        existingCountry.Languages = country.Languages;
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[CountryRepository.UpdateAsync] Error: {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> InsertAsync(List<Country> countries)
        {
            try
            {
                await _context.Set<Country>().AddRangeAsync(countries);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[CountryRepository.InsertAsync] Error: {0}", ex.Message);
                return false;
            }
        }
    }
}
