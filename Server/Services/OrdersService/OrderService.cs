namespace WebAplicationForServices.Server.Services.OrdersService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext context;

        public OrderService(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateOrder(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await context.Orders.FindAsync(orderId);
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await context.Orders.ToListAsync();
        }
    }
}
