using CountriesApi.Application.Common;
using CountriesApi.Application.Interfaces;
using CountriesApi.Application.Mappers;
using CountriesApi.Domain.Entities;
using CountriesApi.Domain.ResponseObjects.DTOs;
using Microsoft.Extensions.Logging;

namespace CountriesApi.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryIntegration _countryIntegration;
        private readonly ILogger<CountryService> _logger;

        public CountryService(ICountryRepository countryRepository,
                              ICountryIntegration countryIntegration,
                              ILogger<CountryService> logger)
        {
            _countryRepository = countryRepository;
            _countryIntegration = countryIntegration;
            _logger = logger;
        }

        public async Task<Result<List<Country>>> GetAllCountries()
        {
            _logger.LogInformation("[CountryService.GetAllCountries] Starting Process of Get All Countries");
            try
            {
                var countries = await _countryRepository.GetAllAsync();
                if (countries != null && countries.Count > 0)
                {
                    return Result<List<Country>>.Success(countries);
                }
                return Result<List<Country>>.Failure("No countries found.", countries);
            }
            catch (Exception ex)
            {
                _logger.LogError("[CountryService.GetAllCountries] Error: {0}", ex.Message);
                return Result<List<Country>>.Failure($"Error:" + ex.Message, new List<Country>());
            }
        }

        public async Task<Result<bool>> UpdateCountries()
        {
            _logger.LogInformation("[CountryService.UpdateCountries] Starting Process of update Countries");
            try
            {
                var countriesDto = await _countryIntegration.GetCountries();
                if (countriesDto.Count == 0)
                {
                    return Result<bool>.Failure("Failed to update countries. Integration didn't found any country.", false);
                }
                var countriesInDataBase = await _countryRepository.GetAllAsync();
                bool successNewEntries = await CountriesToAdd(countriesDto, countriesInDataBase);
                bool successUpdates = await CountriesToUpdate(countriesDto, countriesInDataBase);

                _logger.LogInformation("[CountryService.UpdateCountries] Result new entries: {0}, Result updates:{1}", successNewEntries, successUpdates);
                if (!successUpdates || !successNewEntries)
                {
                    return Result<bool>.Failure("Failed to insert or update countries.", false);
                }
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("[CountryService.UpdateCountries] Error: {0}", ex.Message);
                return Result<bool>.Failure(ex.Message, false);
            }
        }

        private async Task<bool> CountriesToUpdate(List<CountryDto> countriesDto, List<Country> countriesInDataBase)
        {
            bool success = true;
            var countriesToBeUpdated = countriesDto
                .Where(dto =>
                {
                    var matchingCountry = countriesInDataBase.FirstOrDefault(db => db.Name == dto.Name && db.Capital == dto.Capital);

                    if (matchingCountry == null) return false;

                    return
                           matchingCountry.Region != dto.Region ||
                           matchingCountry.SubRegion != dto.SubRegion ||
                           matchingCountry.Population.ToString() != dto.Population.ToString() ||
                           matchingCountry.Currency != dto.Currency;
                })
                .ToList();
            if (countriesToBeUpdated.Count > 0)
            {
                success = await _countryRepository.UpdateAsync(CountryMapper.ToListOfCountries(countriesToBeUpdated));
            }

            return success;
        }

        private async Task<bool> CountriesToAdd(List<CountryDto> countriesDto, List<Country> countriesInDataBase)
        {
            bool success = true;
            var newCountriesFound = countriesDto
                               .Where(dto => !countriesInDataBase.Any(db => db.Name == dto.Name))
                               .ToList();
            if (newCountriesFound?.Count > 0)
            {
                success = await _countryRepository.InsertAsync(CountryMapper.ToListOfCountries(newCountriesFound));
            }
            return success;
        }
    }
}
