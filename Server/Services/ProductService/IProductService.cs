namespace WebAplicationForServices.Server.Services.ProductService
{
    public interface IProductService
    {
         Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task<ServiceResponse<Product>> CreateProduct(Product product, int userId);
        Task<ServiceResponse<bool>> DeleteProduct(int productId);
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
        Task<List<Product>> GetMyProducts(int userId);
    }
}
