namespace OnlineProductStore.Server.Services.ProductService
{
    public interface IProducService
    {
        Task<ServiceResponse<List<Product>>> GetAllProductsAsync();
    }
}
