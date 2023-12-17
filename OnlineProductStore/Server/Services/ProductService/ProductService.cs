
using Microsoft.EntityFrameworkCore;
using OnlineProductStore.Shared;

namespace OnlineProductStore.Server.Services.ProductService
{
    public class ProductService : IProducService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Product>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _dataContext.Products.ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<Product>();

            var product = await _dataContext.Products.FindAsync(id);

            if (product == null)
            {
                response.Success = false;
                response.Message = $"Product does not exist :(";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>();

            var products = await _dataContext.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).ToListAsync();

            response.Data = products;

            return response;
        }

        private async Task<List<Product>> FindProductsBySearchStringAsync(string searchString)
        {
            return await _dataContext.Products.Where(p => p.Title.ToLower().Contains(searchString.ToLower()) ||
                                                               p.Description.ToLower().Contains(searchString.ToLower())).ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> GetSearchSuggestions(string searchString)
        {
            var products = await FindProductsBySearchStringAsync(searchString);

            List<string> suggestions = new List<string>();

            foreach (var product in products)
            {
                if(product.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                {
                    suggestions.Add(product.Title);
                }
            }

            return new ServiceResponse<List<string>>() { Data = suggestions};
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchString)
        {
            var response = new ServiceResponse<List<Product>>();

            response.Data = await FindProductsBySearchStringAsync(searchString);

            return response;
        }
    }
}
