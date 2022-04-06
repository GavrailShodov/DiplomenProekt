namespace WebAplicationForServices.Server.Services.OrdersService
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task DeleteOrder(int orderId);
        Task<List<Order>> GetAllOrders();
        
    }
}
