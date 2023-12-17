namespace OnlineProductStore.Server.Services.ProductService
{
    public interface IProducService
    {
        Task<ServiceResponse<List<Product>>> GetAllProductsAsync();
        Task<ServiceResponse<Product>> GetProductByIdAsync(int id);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);

    }
}
