namespace CountriesApi.Domain.ResponseObjects.DTOs
{
    public class CountryDto
    {
        public string? Name { get; set; }
        public string? Capital { get; set; }
        public string? Region { get; set; }
        public string? SubRegion { get; set; }
        public long Population { get; set; }
        public CoordinateDto Coordinates { get; set; }
        public List<string>? Borders { get; set; }
        public List<string>? Timezones { get; set; }
        public string? Currency { get; set; }
        public List<string>? Languages { get; set; }
        public string? Flag { get; set; }
    }
}
