public class OrderServiceTests
{
    [Fact]
    public async Task PlaceOrder_ShouldCallRepository()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var service = new OrderService(mockRepo.Object);
        var order = new Order();

        // Act
        await service.PlaceOrder(order);

        // Assert
        mockRepo.Verify(r => r.AddAsync(order), Times.Once);
    }
}