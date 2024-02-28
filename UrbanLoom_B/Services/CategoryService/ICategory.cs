using UrbanLoom_B.Dto.CategoryDto;

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
