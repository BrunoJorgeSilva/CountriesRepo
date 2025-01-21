using CountriesApi.Domain.Entities;
using CountriesApi.Domain.ResponseObjects.DTOs;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CountriesApi.Application.Mappers
{
    public static class CountryMapper
    {
        public static Country ToDomain(string? name,
                                       string? capital,
                                       string? region,
                                       string? subRegion,
                                       long population,
                                       decimal latitude,
                                       decimal longitude,
                                       List<string>? borders,
                                       List<string>? timezones,
                                       string? currency,
                                       List<string>? languages)
        {
            return new Country(
            name,
            capital,
            region,
            subRegion,
            population,
            latitude,
            longitude,
            borders,
            timezones,
            currency,
            languages
        );
        }

        public static List<Country> ToListOfCountries(List<CountryDto> dto)
        {
            List <Country> Countries = new List <Country>();
            foreach (CountryDto countryReceived in dto)
            {
                Country country = CountryMapper.ToDomain(countryReceived.Name,
                                                         countryReceived.Capital,
                                                         countryReceived.Region,
                                                         countryReceived.SubRegion,
                                                         countryReceived.Population,
                                                         countryReceived.Coordinates.Latitude,
                                                         countryReceived.Coordinates.Longitude,
                                                         countryReceived.Borders,
                                                         countryReceived.Timezones,
                                                         countryReceived.Currency,
                                                         countryReceived.Languages);
                Countries.Add(country);

            }
            return Countries;
        }
    }
}
