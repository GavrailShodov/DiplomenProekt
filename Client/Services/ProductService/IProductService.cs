namespace WebAplicationForServices.Client.Services.ProductService
{
    public interface IProductService
    {

        event Action ProductsChaged;
        public List<Product> Products { get; set; }

        public string Message { get; set; }

        Task GetProducts();

        Task<ServiceResponse<Product>> GetProductAsync(int productId);

        Task SearchProducts(string searchText);

        Task <List<string>> GetProductSearchSuggestons(string searchText);

        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<List<Product>> GetMyProductsAsync();
    }
}
