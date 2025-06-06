public interface IWeatherApiClient
{
    Task<WeatherResult> GetWeatherAsync(string city);
}

public class WeatherService
{
    private readonly IWeatherApiClient _client;

    public WeatherService(IWeatherApiClient client)
    {
        _client = client;
    }

    public async Task<string> GetWeatherSummaryAsync(string city)
    {
        var result = await _client.GetWeatherAsync(city);
        return $"It is {result.Temperature} C and {result.Condition}";
    }
}