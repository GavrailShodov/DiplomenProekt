using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using WebAplicationForServices.Server.Services.OrdersService;

namespace WebAplicationForServices.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService,IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
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
            var user = await _productService.GetUserByProductId(order.ProductId);
            var product = await _productService.GetProductAsync(order.ProductId);

            MailMessage mail = new MailMessage("justtestemailmg@gmail.com", user.Email, "You have new order for: " + product.Data.Title, "Hi, you have new order on: " + order.ReserveDate.ToString("dd/MM/yyyy") + " at: " + order.Hour + ":00 hour. Your client name is: " + order.Name + ". You can contact him/her on: " + order.Email+" or on phone: "+order.Phone );
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("justtestemailmg@gmail.com", "MgTest123");
            await client.SendMailAsync(mail);

            return Ok(order);
        }
    }
}
