using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.Entity;
using UrbanLoom_B.Entity.Dto;

namespace UrbanLoom_B.Services.CategoryService
{
    public class CategoryService : ICategory
    {
        private readonly DbContextClass _dbContextClass;
        private readonly IMapper _mapper;

        public CategoryService(DbContextClass dbContextClass, IMapper mapper)
        {
            _dbContextClass = dbContextClass;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewDto>> GetCategories()
        {
            var catg = await _dbContextClass.Categories_ul.ToListAsync();
            return _mapper.Map<List<CategoryViewDto>>(catg);
        }
        public async Task<CategoryViewDto> GetCategoryById(int id)
        {
            var catg = await _dbContextClass.Categories_ul.FirstOrDefaultAsync(i=>i.Id == id);
            return _mapper.Map<CategoryViewDto>(catg);
        }

        public async Task AddCategory(CategoryDto category)
        {
            var catg = _mapper.Map<Category>(category);
            _dbContextClass.Categories_ul.Add(catg);
            await _dbContextClass.SaveChangesAsync();
        }

        public async Task UpdateCategory(int id, CategoryDto categoryDto)
        {
            var catg = await _dbContextClass.Categories_ul.FirstOrDefaultAsync(i=>i.Id==id);
            if (catg != null)
            {
                catg.CatagoryName = categoryDto.CatagoryName;
                await _dbContextClass.SaveChangesAsync();
            }
        }

        public async Task DeleteCategory(int id)
        {
            var catg = await _dbContextClass.Categories_ul.FirstOrDefaultAsync(i=>i.Id==id);
            if(catg != null)
            {
                _dbContextClass.Categories_ul.Remove(catg);
                await _dbContextClass.SaveChangesAsync();
            }
        }
    }
}
