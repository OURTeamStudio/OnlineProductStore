
using Microsoft.EntityFrameworkCore;
using OnlineProductStore.Shared;

namespace OnlineProductStore.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Category>>> AddCategory(Category category)
        {
            category.Editing = category.IsNew = false;

            _dataContext.Categories.Add(category);

            await _dataContext.SaveChangesAsync();

            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Category>>> DeleteCategory(int categoryId)
        {
            Category? category = await GetCategoryById(categoryId);

            if (category == null)
            {
                return new ServiceResponse<List<Category>>()
                {
                    Success = false,
                    Message = $"Category with id({categoryId}) is not exists!",
                };
            }

            category.Deleted = true;
            await _dataContext.SaveChangesAsync();

            return await GetAdminCategories();
        }

        private async Task<Category?> GetCategoryById(int categoryId)
        {
            return await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<ServiceResponse<List<Category>>> GetAdminCategories()
        {
            var response = new ServiceResponse<List<Category>>()
            {
                Data = await _dataContext.Categories
                                .Where(c => !c.Deleted)
                                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Category>>> GetAllCategoties()
        {
            var response = new ServiceResponse<List<Category>>()
            {
                Data = await _dataContext.Categories
                                .Where(c => !c.Deleted && c.Visible)
                                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Category>>> UpdateCategory(Category category)
        {
            var categoryToChange = await GetCategoryById(category.Id);

            if (categoryToChange == null)
            {
                return new ServiceResponse<List<Category>>()
                {
                    Success = false,
                    Message = $"Category with id({category.Id}) is not exists!",
                };
            }

            categoryToChange.Name = category.Name;
            categoryToChange.Url = category.Url;
            categoryToChange.Visible = category.Visible;
            
            await _dataContext.SaveChangesAsync();

            return await GetAdminCategories();
        }
    }
}
