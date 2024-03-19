using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.Dto.ProductDto;
using UrbanLoom_B.Entity;
using static System.Net.Mime.MediaTypeNames;

namespace UrbanLoom_B.Services.ProductService
{
    public class ProductService : IProduct
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly DbContextClass _dbContextClass;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string HostUrl;

        public ProductService(IConfiguration configuration, IMapper mapper, DbContextClass dbContextClass, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _mapper = mapper;
            _dbContextClass = dbContextClass;
            _webHostEnvironment = webHostEnvironment;
            HostUrl = _configuration["HostUrl:url"];
        }

        public async Task<List<ProductViewDto>> GetAllProducts()
        {
            var product = await _dbContextClass.Products_ul.Include(p => p.category).ToListAsync();
            if(product.Count > 0)
            {
                var productview = product.Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    ProductImage = HostUrl + p.ProductImage,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    CatagoryName = p.category.CatagoryName
                }).ToList();
                return productview;
            }
            return new List<ProductViewDto>();
        }

        public async Task<ProductViewDto> GetProductById(int id)
        {
            var product = await _dbContextClass.Products_ul.Include(i=>i.category).FirstOrDefaultAsync(x=>x.Id == id);
            if(product != null)
            {
                ProductViewDto productView = new ProductViewDto
                {
                    Id = product.Id,
                    ProductImage = HostUrl + product.ProductImage,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = product.Price,
                    CatagoryName = product.category.CatagoryName

                };
                return productView;
            }
            return new ProductViewDto();
        }

        public async Task<List<ProductViewDto>> GetProductByCategory(string categoryname)
        {
            var products = await _dbContextClass.Products_ul.Include(p=>p.category).Where(i=>i.category.CatagoryName == categoryname).Select(x=> new ProductViewDto
            {
                Id=x.Id,
                ProductImage= HostUrl + x.ProductImage,
                ProductName = x.ProductName,
                ProductDescription = x.ProductDescription,
                Price = x.Price,
                CatagoryName= x.category.CatagoryName
            }).ToListAsync();
            if(products != null)
            {
                return products;
            }
            return new List<ProductViewDto>();
        }

        public async Task<List<ProductViewDto>> SearchProduct(string Name)
        {
            var product = await _dbContextClass.Products_ul.Include(p => p.category).Where(p => p.ProductName.Contains(Name)).ToListAsync();
            if (product != null)
            {
                var productview = product.Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    ProductImage = HostUrl + p.ProductImage,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    CatagoryName = p.category.CatagoryName
                }).ToList();
                return productview;
            }
            return new List<ProductViewDto>();
        }

        public async Task<List<ProductViewDto>> ProductViewPagination(int page = 1, int pageSize = 10)
        {
            var products = await _dbContextClass.Products_ul.Include(p=>p.category).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagepdcts = products.Select(p=> new ProductViewDto 
            {
                Id = p.Id,
                ProductImage = HostUrl + p.ProductImage,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                CatagoryName = p.category.CatagoryName
            }).ToList();
            return pagepdcts;
        }

        public async Task AddProduct(ProductDto product, IFormFile Img)
        {
            try
            {
                string ProductImage = null;
                if(Img != null && Img.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload", "Product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Img.CopyToAsync(stream);
                    }
                    ProductImage = "/Upload/Product/" + fileName;
                }
                else
                {
                    ProductImage = "/Upload/common/noimage.png";
                }

                var prodimg = _mapper.Map<Product>(product);
                prodimg.ProductImage = ProductImage;

                await _dbContextClass.Products_ul.AddAsync(prodimg);
                await _dbContextClass.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                throw new Exception("Error adding product: " + ex.Message);
            }
        }
        public async Task UpdateProduct(int id , ProductDto product, IFormFile Img)
        {
            try
            {
                var prod = await _dbContextClass.Products_ul.FirstOrDefaultAsync(p => p.Id == id);
                if(prod != null)
                {
                    prod.ProductName = product.ProductName;
                    prod.ProductDescription = product.ProductDescription;
                    prod.Price = product.Price;
                    prod.CategoryId = product.CategoryId;

                    if (Img != null && Img.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload", "Product", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Img.CopyToAsync(stream);
                        }

                        prod.ProductImage = "/Upload/Product/" + fileName;
                    }

                    await _dbContextClass.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Product with ID {id} not found.");
                }
            }catch(Exception ex)
            {
                throw new Exception($"Error updating product with ID {id}: {ex.Message}");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = await _dbContextClass.Products_ul.FirstOrDefaultAsync(i => i.Id == id);
                _dbContextClass.Products_ul.Remove(product);
                await _dbContextClass.SaveChangesAsync();
            }catch(Exception ex) 
            {
                throw new Exception($"Error Deleting product with ID {id}: {ex.Message}");
            }
        }
    }
}
