namespace CountriesApi.Domain.Entities
{
    public class Country
    { 
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Capital { get; set; }
        public string? Region { get; set; }
        public string? SubRegion { get; set; }
        public long Population { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Borders { get; set; }
        public string? Timezones { get; set; }
        public string? Currency {  get; set; }
        public string? Languages { get; set; }

        public Country() { }
        public Country(
        string? name,
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
            Name = name;
            Capital = capital;
            Region = region;
            SubRegion = subRegion;
            Population = population;
            Latitude = latitude;
            Longitude = longitude;
            Borders = borders != null ? string.Join(", ", borders) : null;
            Timezones = timezones != null ? string.Join(", ", timezones) : null;
            Currency = currency;
            Languages = languages != null ? string.Join(", ", languages) : null;
        }
    }
}
