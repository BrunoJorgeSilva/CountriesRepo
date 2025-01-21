using JobLibrary.Interfaces;

public class UpdateJob : IUpdateJob
{
    public async Task ExecuteAsync()
    {
        HttpClient _httpClient = new HttpClient();
        var url = "https://localhost:44367/api/Countries/UpdateAllCountries"; 
        var response = await _httpClient.PostAsync(url, null); 

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Update successful.");
        }
        else
        {
            Console.WriteLine($"Failed to update countries. Status code: {response.StatusCode}");
        }
    }
}