using System.Net.Http.Json;

namespace OnlineProductStore.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        public event Action? ProductsChanged = null;

        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; }
        public List<Product> AdminProducts { get; set; }

        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");

            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
            await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if (result != null && result.Data != null)
                Products = result.Data;

            ProductsChanged?.Invoke();
        }

        public async Task SearchProducts(string searchString)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchString}");

            if(result != null && result.Data != null)
                Products = result.Data;

            if(Products.Count == 0) Message = "No products found.";

            ProductsChanged?.Invoke();
        }

        public async Task<List<string>> GetSearchSuggestions(string searchString)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/search-suggestions/{searchString}");
            var suggestions = new List<string>();

            if(result != null && result.Data != null)
                suggestions = result.Data;

            return suggestions;
        }

        public async Task GetAdminProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/admin");

            AdminProducts = result.Data;

            if (AdminProducts.Count == 0)
                Message = "Products not found";
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var result = await _httpClient.PostAsJsonAsync("api/product", product);

            var createdProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;

            return createdProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await _httpClient.PutAsJsonAsync("api/product", product);

            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        }

        public async Task DeleteProduct(Product product)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/product/{product.Id}", product);
        }
    }
}
