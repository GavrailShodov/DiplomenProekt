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

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductSerch(searchText);
            var result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = product.Description.Split().Select(s => s.Trim());

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word)) 
                        {
                            result.Add(word);
                        }
                    }
                }

        }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
        {
            var responce = new ServiceResponse<List<Product>>()
            {
                Data = await FindProductSerch(searchText)
            };
            return responce;
        }

        private async Task<List<Product>> FindProductSerch(string searchText)
        {
            return await context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower()))
                            .ToListAsync();
        }
    }
}
