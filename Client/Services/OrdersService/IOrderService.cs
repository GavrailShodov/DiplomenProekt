namespace WebAplicationForServices.Client.Services.OrdersService
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();

        Task CreateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
    }
}
