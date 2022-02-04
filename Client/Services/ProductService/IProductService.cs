namespace WebAplicationForServices.Client.Services.ProductService
{
    public interface IProductService
    {
        public List<Product> Products { get; set; }

        Task GetProducts();

        Task<ServiceResponse<Product>> GetProductAsync(int productId);
    }
}
