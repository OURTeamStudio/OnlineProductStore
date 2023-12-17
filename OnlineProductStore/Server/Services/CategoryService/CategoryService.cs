
using Microsoft.EntityFrameworkCore;

namespace OnlineProductStore.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Category>>> GetAllCategoties()
        {
            var response = new ServiceResponse<List<Category>>()
            {
                Data = await _dataContext.Categories.ToListAsync()
            };

            return response;
        }
    }
}
