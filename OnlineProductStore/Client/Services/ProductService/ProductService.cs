using System.Net.Http.Json;

namespace OnlineProductStore.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        public event Action? ProductsChanged = null;

        public List<Product> Products { get; set; } = new List<Product>();

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
    }
}
