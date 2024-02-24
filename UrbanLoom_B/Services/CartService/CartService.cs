using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.Entity;
using UrbanLoom_B.Entity.Dto;
using UrbanLoom_B.JWT;

namespace UrbanLoom_B.Services.CartService
{
    public class CartService : ICart
    {
        private readonly IConfiguration _configuration;
        private readonly IJwt _jwt;
        private readonly DbContextClass _dbContextClass;
        private readonly string HostUrl;

        public CartService(IConfiguration configuration, IJwt jwt, DbContextClass dbContextClass)
        {
            _configuration = configuration;
            _jwt = jwt;
            _dbContextClass = dbContextClass;
            HostUrl = _configuration["HostUrl:url"];
        }

        public async Task<List<CartViewDto>> GetCartItems(string token)
        {
            try
            {
                var userID = _jwt.GetUserIdFromToken(token);
                if (userID == null)
                {
                    throw new Exception("user id not valid");
                }

                var user = await _dbContextClass.Cart_ul.Include(i => i.cartitem).ThenInclude(p => p.products).FirstOrDefaultAsync(I => I.UserId == userID);
                if (user != null)
                {
                    var cartitwms = user.cartitem.Select(i => new CartViewDto
                    {
                        ProductId = i.Id,
                        ProductName = i.products.ProductName,
                        Quantity = i.Quantity,
                        Price = i.products.Price,
                        TotalAmount=i.products.Price * i.Quantity,
                        ProductImage = HostUrl+i.products.ProductImage
                    }).ToList();
                    return cartitwms; 
                }
                return new List<CartViewDto>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task AddToCart(string token, int productId)
        {
            var userid = _jwt.GetUserIdFromToken(token);
            if(userid == null)
            {
                throw new Exception("user id is not valid");
            }
            var user = await _dbContextClass.Users_ul.Include(c=>c.cart).ThenInclude(ci=>ci.cartitem).FirstOrDefaultAsync(i=>i.Id == userid);
            var product = await _dbContextClass.Products_ul.FirstOrDefaultAsync(p=>p.Id == productId);
            if(user !=  null && product != null)
            {
                if(user.cart == null)
                {
                    user.cart = new Cart
                    {
                        UserId = userid,
                        cartitem = new List<CartItem>()
                    };
                    _dbContextClass.Cart_ul.Add(user.cart);
                    await _dbContextClass.SaveChangesAsync();
                }

                var itemexist = user.cart.cartitem.FirstOrDefault(i => i.ProductId == productId);
                if(itemexist != null)
                {
                    itemexist.Quantity = itemexist.Quantity + 1;
                }
                else
                {
                    var newCartItem = new CartItem
                    {
                        CartId = user.cart.Id,
                        ProductId = productId,
                        Quantity = 1
                    };
                    _dbContextClass.Cartitem_ul.Add(newCartItem);
                }
                await _dbContextClass.SaveChangesAsync();
            }

        }

        public async Task QuantityINC(string token, int productId)
        {
            var userID = _jwt.GetUserIdFromToken(token);
            if (userID == null)
            {
                throw new Exception("user id not valid");
            }

            var user = await _dbContextClass.Users_ul.Include(c => c.cart).ThenInclude(ci => ci.cartitem).FirstOrDefaultAsync(id=>id.Id==userID);
            var product = await _dbContextClass.Products_ul.FirstOrDefaultAsync(i=>i.Id==productId);
            if(user != null && product != null)
            {
                var item = user.cart.cartitem.FirstOrDefault(i=>i.ProductId==productId);
                if(item != null)
                {
                    item.Quantity = item.Quantity + 1;
                    await _dbContextClass.SaveChangesAsync();
                }
            }
        }

        public async Task QuantityDEC(string token, int productId)
        {
            var userID = _jwt.GetUserIdFromToken(token);
            if (userID == null)
            {
                throw new Exception("user id not valid");
            }

            var user = await _dbContextClass.Users_ul.Include(c => c.cart).ThenInclude(ci => ci.cartitem).FirstOrDefaultAsync(id => id.Id == userID);
            var product = await _dbContextClass.Products_ul.FirstOrDefaultAsync(i => i.Id == productId);
            if (user != null && product != null)
            {
                var item = user.cart.cartitem.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    item.Quantity = item.Quantity >= 1 ? item.Quantity-1 : item.Quantity;
                    if(item.Quantity == 0)
                    {
                        _dbContextClass.Cartitem_ul.Remove(item);
                        await _dbContextClass.SaveChangesAsync();
                    }
                    await _dbContextClass.SaveChangesAsync();
                }
                await _dbContextClass.SaveChangesAsync();
            }
        }

        public async Task DeleteCart(string token, int productId)
        {
            var userID = _jwt.GetUserIdFromToken(token);
            if (userID == null)
            {
                throw new Exception("user id not valid");
            }
            var user = await _dbContextClass.Users_ul.Include(c => c.cart).ThenInclude(ci => ci.cartitem).FirstOrDefaultAsync(id => id.Id == userID);
            var product = await _dbContextClass.Products_ul.FirstOrDefaultAsync(i => i.Id == productId);
            if(product != null && user != null)
            {
                var item = user.cart.cartitem.FirstOrDefault(i=>i.ProductId == productId);
                _dbContextClass.Cartitem_ul.Remove(item);
                await _dbContextClass.SaveChangesAsync();
            }
        }

    }
}
