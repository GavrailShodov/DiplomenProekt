using Microsoft.EntityFrameworkCore;
using WebAplicationForServices.Server.Data;

namespace WebAplicationForServices.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext context;

        public ProductService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
           var responce =  new ServiceResponse<Product>();
            var product = await context.Products.FindAsync(productId);
            if(product == null)
            {
                responce.Success = false;
                responce.Message = "This product does not exist.";
            }
            else
            {
                responce.Data = product;
            }

            return responce;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var responce = new ServiceResponse<List<Product>>()
            {
                Data =  await context.Products.ToListAsync()
            };

            return responce;
        }

           
        
    }
}
