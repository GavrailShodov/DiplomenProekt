using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAplicationForServices.Server.Services.OrdersService;

namespace WebAplicationForServices.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpDelete("{orderId}")]
        public async Task DeleteOrders(int orderId)
        {
            await _orderService.DeleteOrder(orderId);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            await _orderService.CreateOrder(order);
            return Ok(order);
        }
    }
}
