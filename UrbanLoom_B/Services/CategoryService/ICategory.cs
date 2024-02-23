using UrbanLoom_B.Entity.Dto;

namespace UrbanLoom_B.Services.CategoryService
{
    public interface ICategory
    {
        Task<List<CategoryViewDto>> GetCategories();
        Task<CategoryViewDto> GetCategoryById(int id);

        Task AddCategory(CategoryDto category);
        Task UpdateCategory(int id, CategoryDto categoryDto);
        Task DeleteCategory(int id);
    }
}
