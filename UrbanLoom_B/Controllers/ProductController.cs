using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using UrbanLoom_B.Entity.Dto;
using UrbanLoom_B.Services.ProductService;

namespace UrbanLoom_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProduct product, IWebHostEnvironment webHostEnvironment)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("ALL PRODUCTS")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                var product = await _product.GetAllProducts();
                return Ok(product);
            }catch(Exception ex) 
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("PRODUCT BY {id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _product.GetProductById(id);
                return Ok(product);
            }catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("PRODUCT BY CATEGORY/{Category}")]
        public async Task<ActionResult> GetProductByCategory(string Category) 
        {
            try
            {
                var products = await _product.GetProductByCategory(Category);
                return Ok(products);
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpGet("PAGE")]
        public async Task<ActionResult> GetProductByPage(int page = 1 , int pagesize = 10)
        {
            try
            {
                var products = await _product.ProductViewPagination(page, pagesize);
                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpGet("SEARCH")]
        public async Task<ActionResult> SearchProduct(string Name)
        {
            try
            {
                var product = await _product.SearchProduct(Name);
                return Ok(product);
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPost("ADD PRODUCT")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddProduct([FromForm] ProductDto product, IFormFile Img)
        {
            try
            {
                await _product.AddProduct(product, Img);
                return Ok("Product Added Sucessfully");
            }catch (Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpPut("UPDATE PRODUCT")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateProduct(int id,[FromForm] ProductDto product, IFormFile Img)
        {
            try
            {
                await _product.UpdateProduct(id, product, Img);
                return Ok("Product Updated Sucessfully");
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpDelete("DELETE PRODUCT")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _product.DeleteProduct(id);
                return Ok("Product Deleted Sucessfully");
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

    }
}
