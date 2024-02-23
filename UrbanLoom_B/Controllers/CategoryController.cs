using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanLoom_B.Entity.Dto;
using UrbanLoom_B.Services.CategoryService;

namespace UrbanLoom_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICategory _category;
        public CategoryController(IConfiguration configuration, ICategory category)
        {
            _configuration = configuration;
            _category = category;
        }

        [HttpGet("CATEGORIES")]
        public async Task<IActionResult> GetCatg() 
        {
            try
            {
                return Ok(await _category.GetCategories());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CATEGORY")]
        public async Task<IActionResult> GetCatById(int id)
        {
            try
            {
                return Ok(await _category.GetCategoryById(id));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ADD CATEGORY")]
        public async Task<ActionResult> AddCateg([FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _category.AddCategory(categoryDto);
                return Ok("Category added sucessfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id,[FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _category.UpdateCategory(id, categoryDto);
                return Ok("Category Updated sucessfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _category.DeleteCategory(id);
                return Ok("Category Deleted sucessfully");
            }catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
