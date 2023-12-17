namespace OnlineProductStore.Client.Services.CategodyService
{
    public interface ICategoryService
    {
        List<Category> Categories { get ; set; }

        Task GetCategories();
    }
}
