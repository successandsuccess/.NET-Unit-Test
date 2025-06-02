using DemoApp;
using Xunit;

namespace DemoApp.Tests;

public class CalculatorServiceTests
{
    private readonly CalculatorService _service = new();

    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        var result = _service.Add(3, 4);
        Assert.Equal(7, result); // PASS
    }

    [Fact]
    public void Subtract_ReturnsIncorrectValue_ShouldFail()
    {
        var result = _service.Subtract(10, 5);
        Assert.Equal(7, result); // FAIL (actual is 5)
    }

    [Fact]
    public void Divide_ValidInputs_ReturnsQuotient()
    {
        var result = _service.Divide(20, 4);
        Assert.Equal(5, result); // PASS
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        Assert.Throws<DivideByZeroException>(() => _service.Divide(5, 0)); // PASS
    }
}
