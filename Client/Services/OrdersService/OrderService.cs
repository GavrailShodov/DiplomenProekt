using System.Net.Http.Json;

namespace WebAplicationForServices.Client.Services.OrdersService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient http;

        public OrderService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var result = await http.GetFromJsonAsync<List<Order>>("api/order");
            return result;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await http.PostAsJsonAsync($"api/order",order);
        }

        public async Task DeleteOrderAsync(Order order)
        {
            await http.DeleteAsync($"api/order/{order.Id}");
        }
    }

}
