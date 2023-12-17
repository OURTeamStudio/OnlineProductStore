namespace OnlineProductStore.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action? ProductsChanged;

        List<Product> Products { get; set; }
        string Message { get; set; }

        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProductById(int id);
        Task SearchProducts(string searchString);
        Task<List<string>> GetSearchSuggestions(string searchString);
    }
}
