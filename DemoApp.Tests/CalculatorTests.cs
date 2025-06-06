public class CalculatorTests
{
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, -1, -2)]
    public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
    {
        // Arrange
        var calculator = new CalculatorTests();
        // Act
        var result = calculator.Add(a, b);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ByZero_ShouldThrowException()
    {
        // Arrange
        var calculator = new Calculator();

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(5, 0));
    }
}