
using Microsoft.EntityFrameworkCore;
using OnlineProductStore.Shared;

namespace OnlineProductStore.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<Product>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _dataContext.Products.Where(p => !p.Deleted && p.Visible).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<Product>();

            Product product = null;
            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
            }
            else
            {
                product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id && !p.Deleted && p.Visible);
            }

            if (product == null)
            {
                response.Success = false;
                response.Message = $"Product with id({id}) does not exist :(";
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

            var products = await _dataContext.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) && p.Visible).ToListAsync();

            response.Data = products;

            return response;
        }

        private async Task<List<Product>> FindProductsBySearchStringAsync(string searchString)
        {
            return await _dataContext.Products.Where(p => p.Title.ToLower().Contains(searchString.ToLower()) ||
                                                               p.Description.ToLower().Contains(searchString.ToLower()) &&
                                                               !p.Deleted && p.Visible).ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> GetSearchSuggestions(string searchString)
        {
            var products = await FindProductsBySearchStringAsync(searchString);

            List<string> suggestions = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                {
                    suggestions.Add(product.Title);
                }
            }

            return new ServiceResponse<List<string>>() { Data = suggestions };
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchString)
        {
            var response = new ServiceResponse<List<Product>>();

            response.Data = await FindProductsBySearchStringAsync(searchString);

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetAdminProducts()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _dataContext.Products.Where(p => !p.Deleted).ToListAsync(),
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> CreateProduct(Product product)
        {
            _dataContext.Products.Add(product);

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<Product>() { Data = product };
        }

        public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            var productToUpdate = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id && !p.Deleted);

            if (productToUpdate == null)
            {
                return new ServiceResponse<Product>()
                {
                    Success = false,
                    Message = $"Product with id({product.Id}) is not exists!"
                };
            }

            productToUpdate.Title = product.Title;
            productToUpdate.Description = product.Description;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.ImageUrl = product.ImageUrl;
            productToUpdate.Price = product.Price;
            productToUpdate.Visible = product.Visible;

            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<Product>() { Data = product };
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
            var productToDelete = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (productToDelete == null)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    Success = false,
                    Message = $"Product with id({productId}) is not exists!"
                };
            }

            productToDelete.Deleted = true;
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>() { Data = true };
        }
    }
}
