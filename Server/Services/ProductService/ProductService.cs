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
            var responce = new ServiceResponse<Product>();
            var product = await context.Products.FindAsync(productId);
            if (product == null)
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
               Data =  await context.Products
                    .Where(p => !p.Deleted)
                    .ToListAsync()
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

        public async Task<ServiceResponse<Product>> CreateProduct(Product product,int userId)
        {
            product.UserId = userId;
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
            var dbProduct = await context.Products.FindAsync(productId);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Deleted = true;

            await context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            var dbProduct = await context.Products.FindAsync(product.Id);
            if (dbProduct == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.Price = product.Price;
            dbProduct.Deleted = product.Deleted;

            await context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<List<Product>> GetMyProducts(int userId)
        {
            var products = await context.Products.Where(u => u.UserId == userId).ToListAsync();
            var result = products.Where(p => !p.Deleted).ToList();
            return result;
        }

        public async Task<User> GetUserByProductId(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            var result = await context.Users.FindAsync(product.UserId);

            return result;
        }
    }
    }
