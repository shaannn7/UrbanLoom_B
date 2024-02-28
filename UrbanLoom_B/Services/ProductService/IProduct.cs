using UrbanLoom_B.Dto.ProductDto;

namespace UrbanLoom_B.Services.ProductService
{
    public interface IProduct
    {
        Task<List<ProductViewDto>> GetAllProducts();
        Task<ProductViewDto> GetProductById(int id);
        Task<List<ProductViewDto>> GetProductByCategory(string categoryname);
        Task<List<ProductViewDto>> SearchProduct(string Name);
        Task<List<ProductViewDto>> ProductViewPagination(int page = 1, int pageSize = 10);   
        Task AddProduct(ProductDto product , IFormFile Img);
        Task UpdateProduct(int id , ProductDto product , IFormFile Img);
        Task DeleteProduct(int id);
    }
}
