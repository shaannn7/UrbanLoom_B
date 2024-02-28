using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.Dto.WhishListDto;
using UrbanLoom_B.Entity;
using UrbanLoom_B.JWT;

namespace UrbanLoom_B.Services.WhishListService
{
    public class WhishListService : IWhishList
    {
        private readonly DbContextClass _dbContextClass;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string HostURL;
        private readonly IJwt _jwt;

        public WhishListService(DbContextClass dbContextClass, IMapper mapper, IConfiguration configuration,  IJwt jwt)
        {
            _dbContextClass = dbContextClass;
            _mapper = mapper;
            _configuration = configuration;
            HostURL = _configuration["HostUrl:url"];
            _jwt = jwt;
        }

        public async Task<List<WhishListViewDto>> GetWhishList(string token)
        {
            var userID = _jwt.GetUserIdFromToken(token);
            if(userID == null)
            {
                throw new Exception("user id is not valid");
            }

            var WhishListView = await _dbContextClass.Whishlist_ul.Include(i=>i.products).ThenInclude(o=>o.category).Where(u=>u.UserId==userID).ToListAsync();
            if(WhishListView != null)
            {
                var viewWhishList = WhishListView.Select(i => new WhishListViewDto
                {
                    Id = i.Id,
                    ProductName = i.products?.ProductName,
                    ProductDescription = i.products?.ProductDescription,
                    Price = i.products.Price,
                    CatagoryName = i.products?.category.CatagoryName,
                    ProductImage = HostURL+i.products.ProductImage
                }).ToList();
                return viewWhishList;
            }else
            {
                return new List<WhishListViewDto>();
            }
        }

        public async Task<bool> AddToWhishList(string Token, int ProductID)
        {
            int userId = _jwt.GetUserIdFromToken(Token);
            if (userId == null)
            {
                throw new Exception("user id is not valid");
            }
            var WhishListExist = _dbContextClass.Whishlist_ul.Include(i=>i.products).FirstOrDefault(o=>o.ProductId==ProductID && o.UserId == userId);
            if (WhishListExist == null)
            {
                WhishLIstDto whishLIstDto = new WhishLIstDto
                {
                    ProductId = ProductID,
                    UserId = userId
                };
                var WhishListMapper = _mapper.Map<WhishList>(whishLIstDto);
                _dbContextClass.Whishlist_ul.Add(WhishListMapper);
                await _dbContextClass.SaveChangesAsync();
                return true;
            }
            _dbContextClass.Whishlist_ul.Remove(WhishListExist);
            await _dbContextClass.SaveChangesAsync();
            return true;
        }

        public async Task RemoveWhishList(string Token, int productID)
        {
            int Userid = _jwt.GetUserIdFromToken(Token);
            if (Userid == null)
            {
                throw new Exception("user id is not valid");
            }
            var WhishList = await _dbContextClass.Whishlist_ul.Include(i => i.products).FirstOrDefaultAsync(p => p.ProductId == productID && p.UserId == Userid);
            if (WhishList != null)
            {
                _dbContextClass.Whishlist_ul.Remove(WhishList);
                await _dbContextClass.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product id is not valid");
            }
        }
    }
}
