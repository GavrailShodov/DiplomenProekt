

namespace WebAplicationForServices.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient http;

        public ProductService(HttpClient http)
        {
            this.http = http;
        }
        public List<Product> Products { get; set; } = new List<Product>();

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");

            return result;
        }

        public async Task GetProducts()
        {
            var resullt = await http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
            if (resullt != null && resullt.Data != null)
            {
                Products = resullt.Data;
            }
        }
    }
}
