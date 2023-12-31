﻿namespace OnlineProductStore.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action? ProductsChanged;

        List<Product> Products { get; set; }
        List<Product> AdminProducts { get; set; }
        string Message { get; set; }

        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProductById(int id);
        Task SearchProducts(string searchString);
        Task<List<string>> GetSearchSuggestions(string searchString);
        Task GetAdminProducts();
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        
    }
}
