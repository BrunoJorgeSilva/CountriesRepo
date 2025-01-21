using Moq;
using Microsoft.Extensions.Logging;
using CountriesApi.Application.Interfaces;
using CountriesApi.Application.Services;
using CountriesApi.Domain.Entities;
using CountriesApi.Domain.ResponseObjects.DTOs;

public class CountryServiceTests
{
    private readonly Mock<ICountryRepository> _mockCountryRepository;
    private readonly Mock<ICountryIntegration> _mockCountryIntegration;
    private readonly Mock<ILogger<CountryService>> _mockLogger;
    private readonly CountryService _countryService;

    public CountryServiceTests()
    {
        _mockCountryRepository = new Mock<ICountryRepository>();
        _mockCountryIntegration = new Mock<ICountryIntegration>();
        _mockLogger = new Mock<ILogger<CountryService>>();

        _countryService = new CountryService(
            _mockCountryRepository.Object,
            _mockCountryIntegration.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task GetAllCountries_ShouldReturnSuccess_WhenCountriesExist()
    {
        var countries = new List<Country>
        {
            new Country { Id = 1, Name = "Country A" },
            new Country { Id = 2, Name = "Country B" }
        };

        _mockCountryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(countries);

        var result = await _countryService.GetAllCountries();

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("Country A", result.Value[0].Name);
        Assert.Equal("Country B", result.Value[1].Name);
    }

    [Fact]
    public async Task GetAllCountries_ShouldReturnFailure_WhenNoCountriesExist()
    {
        var countries = new List<Country>();

        _mockCountryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(countries);

        var result = await _countryService.GetAllCountries();

        Assert.False(result.IsSuccess);
        Assert.Equal("No countries found.", result.ErrorMessage);
    }

    [Fact]
    public async Task GetAllCountries_ShouldReturnFailure_WhenExceptionOccurs()
    {
        var exceptionMessage = "Database connection error";

        _mockCountryRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new System.Exception(exceptionMessage));

        var result = await _countryService.GetAllCountries();

        Assert.False(result.IsSuccess);
        Assert.Contains(exceptionMessage, result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateCountries_ReturnsFailure_WhenNoCountriesFound()
    {
        // Arrange
        var countriesDto = new List<CountryDto>(); 
        _mockCountryIntegration.Setup(x => x.GetCountries()).ReturnsAsync(countriesDto);

        var countriesInDataBase = new List<Country>(); 
        _mockCountryRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(countriesInDataBase);

        // Act
        var result = await _countryService.UpdateCountries();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Failed to update countries. Integration didn't found any country.", result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateCountries_ReturnsFailure_WhenInsertOrUpdateFails()
    {
        // Arrange
        var countriesDto = new List<CountryDto>
    {
        new CountryDto { Name = "Country1", Capital = "Capital1" }
    };

        var countriesInDataBase = new List<Country>
    {
        new Country { Name = "Country1", Capital = "Capital1", Region = "Region1", SubRegion = "SubRegion1", Population = 1000, Currency = "USD" }
    };

        _mockCountryIntegration.Setup(x => x.GetCountries()).ReturnsAsync(countriesDto);
        _mockCountryRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(countriesInDataBase);
        _mockCountryRepository.Setup(x => x.UpdateAsync(It.IsAny<List<Country>>())).ReturnsAsync(false);
        _mockCountryRepository.Setup(x => x.InsertAsync(It.IsAny<List<Country>>())).ReturnsAsync(false);

        // Act
        var result = await _countryService.UpdateCountries();

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCountries_ReturnsSuccess_WhenUpdateAndInsertSucceed()
    {
        // Arrange
        var countriesDto = new List<CountryDto>
    {
        new CountryDto { Name = "Country1", Capital = "Capital1", Region = "Region1", SubRegion = "SubRegion1", Population = 1000, Currency = "USD" }
    };

        var countriesInDataBase = new List<Country>
    {
        new Country { Name = "Country1", Capital = "Capital1", Region = "Region1", SubRegion = "SubRegion1", Population = 1000, Currency = "USD" }
    };

        _mockCountryIntegration.Setup(x => x.GetCountries()).ReturnsAsync(countriesDto);
        _mockCountryRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(countriesInDataBase);
        _mockCountryRepository.Setup(x => x.UpdateAsync(It.IsAny<List<Country>>())).ReturnsAsync(true);
        _mockCountryRepository.Setup(x => x.InsertAsync(It.IsAny<List<Country>>())).ReturnsAsync(true);

        // Act
        var result = await _countryService.UpdateCountries();

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateCountries_ReturnsFailure_WhenExceptionOccurs()
    {
        // Arrange
        var exceptionMessage = "Test exception";

        _mockCountryIntegration.Setup(x => x.GetCountries()).ThrowsAsync(new System.Exception(exceptionMessage));

        // Act
        var result = await _countryService.UpdateCountries();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(exceptionMessage, result.ErrorMessage);
    }
}
