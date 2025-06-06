public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherSummaryAsync_ReturnsExpectedSummary()
    {
        // Arrange
        var mockClient = new Mock<IWeatherApiClient>();
        mockClient
            .Setup(c => c.GetWeatherAsync("London"))
            .ReturnsAsync(new WeatherResult { Temperature = 20, Condition = "Sunny" });

        var service = new WeatherService(mockClient.Object);

        // Act
        var summary = await service.GetWeatherSummaryAsync("London");

        // Assert
        Assert.Equal("It is 20'c and Sunny", summary);
    }
}
