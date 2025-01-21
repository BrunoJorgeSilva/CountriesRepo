using CountriesApi.Domain.ResponseObjects.DTOs;

namespace CountriesApi.Domain.ResponseObjects
{
    public class ApiResponse
    {
        public string? Message { get; set; }
        public List<CountryDto>? Data { get; set; }
        public int StatusCode { get; set; }
    }
}
