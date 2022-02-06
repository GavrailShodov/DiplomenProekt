

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
        public string Message { get; set; } = "Loading Products ...";

        public event Action ProductsChaged;

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
            ProductsChaged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestons(string searchText)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestons/{searchText}");

            return result.Data;
        }

        public async Task SearchProducts(string searchText)
        {
            var result = await http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchText}");
            if(result != null && result.Data != null)
            Products = result.Data;
            if(Products.Count == 0 )
            {
                Message = "No products found!";
            }
            ProductsChaged.Invoke();
           
        }
    }
}
