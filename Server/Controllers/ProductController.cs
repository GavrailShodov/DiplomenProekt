using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAplicationForServices.Server.Data;

namespace WebAplicationForServices.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]

        public  async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var result = await productService.GetProductsAsync();
             
            return Ok(result);
        }

        [HttpGet("{productId}")]

        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
        {
            var result = await productService.GetProductAsync(productId);

            return Ok(result);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> SearchProducts(string searchText)
        {
            var result = await productService.SearchProducts(searchText);
            return Ok(result);
        }

        [HttpGet("searchsuggestons/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductSearchSuggestions(string searchText)
        {
            var result = await productService.GetProductSearchSuggestions(searchText);
            return Ok(result);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateProduct(Product product)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await productService.CreateProduct(product,userId);
            return Ok(result);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(Product product)
        {
            var result = await productService.UpdateProduct(product);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(int id)
        {
            var result = await productService.DeleteProduct(id);
            return Ok(result);
        }

        [HttpGet("myproducts"), Authorize]
        public async Task<List<Product>> GetMyProducts()
        {
            var result = await productService.GetMyProducts(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return result;
        }
            
    }
}
