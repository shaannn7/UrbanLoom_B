using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.Entity;
using UrbanLoom_B.Entity.Dto;
using UrbanLoom_B.JWT;

namespace UrbanLoom_B.Services.OrderService
{
    public class OrderService : IOrder
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextClass _dbContextClass;
        private readonly string HostUrl;
        private readonly IJwt _jwt;

        public OrderService(IConfiguration configuration, DbContextClass dbContextClass, IJwt jwt)
        {
            _configuration = configuration;
            _dbContextClass = dbContextClass;
            HostUrl = _configuration["HostUrl:url"];
            _jwt = jwt;
        }


        ///   BUY FROM CART ///
        public async Task<bool> CreateOrderFromCart(string token, OrderRequestDto orderRequest)
        {
           
                int UserId = _jwt.GetUserIdFromToken(token);
                if(UserId == null)
                {
                    throw new Exception("user id is not valid");
                }

                var cartorder = await _dbContextClass.Cart_ul.Include(ci=>ci.cartitem).ThenInclude(p=>p.products).FirstOrDefaultAsync(i=>i.UserId == UserId);
                if (cartorder == null)
                {
                   throw new Exception("Cart not found for the user.");
                }
                var order = new Order
                {
                    userId = UserId,
                    OrderDate = DateTime.Now,
                    CustomerCity = orderRequest.CustomerCity,
                    CustomerEmail = orderRequest.CustomerEmail,
                    CustomerPhone = orderRequest.CustomerPhone,
                    HomeAddress = orderRequest.HomeAddress,
                    CustomerName = orderRequest.CustomerName,
                    OrderStatus = "pending",
                    OrderItems = cartorder.cartitem.Select(ci => new OrderItem
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        TotalPrice = ci.Quantity * ci.products.Price
                    }).ToList()
                };

                await _dbContextClass.Orders_ul.AddAsync(order);
                _dbContextClass.Cart_ul.Remove(cartorder);
                await _dbContextClass.SaveChangesAsync();
                return true;

          
        }

        ///   BUY FROM SHOP ///

        public async Task<bool> CreateOrderFromShop(string token, OrderRequestDto orderRequest , int productid)
        {
            try
            {
                int UserID = _jwt.GetUserIdFromToken(token);
                if (UserID == null)
                {
                    throw new Exception("user id is not valid");

                }
                var productorder = await _dbContextClass.Products_ul.FirstOrDefaultAsync(i=>i.Id == productid);
                var order = new Order
                {
                    userId = UserID,
                    OrderDate = DateTime.Now,
                    CustomerCity = orderRequest.CustomerCity,
                    CustomerEmail = orderRequest.CustomerEmail,
                    CustomerPhone = orderRequest.CustomerPhone,
                    HomeAddress = orderRequest.HomeAddress,
                    CustomerName = orderRequest.CustomerName,
                    OrderStatus = "Pending",
                    OrderItems = new List<OrderItem>()

                };
                _dbContextClass.Orders_ul.Add(order);
                await _dbContextClass.SaveChangesAsync();

                var orderitem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = productid,
                    Quantity = 1,
                    TotalPrice = productorder.Price
                };
                await _dbContextClass.Orderitems_ul.AddAsync(orderitem);
                await _dbContextClass.SaveChangesAsync();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }

        ///  ADMIN ///
        public async Task<List<OrderAdminView>> AdminGetAllOrders()
        {
            var order = await _dbContextClass.Orders_ul.Include(oi=>oi.OrderItems).ToListAsync();
            if( order == null )
            {
                return new List<OrderAdminView>();
            }
            var allorders = order.Select(o=> new OrderAdminView
            {
                Id = o.Id,
                CustomerEmail = o.CustomerEmail,
                CustomerName = o.CustomerName,
                OrderStatus = o.OrderStatus,
                OrderDate = o.OrderDate,
            }).ToList();
            return allorders;
        }

        public async Task<OrderAdminDetailViewDto> GetDetailedOrderDetailsByOrderID(int orderid)
        {
                var order = await _dbContextClass.Orders_ul.Include(oi => oi.OrderItems).ThenInclude(p=>p.products).FirstOrDefaultAsync(o=>o.Id == orderid);
                if( order != null)
                {
                    var orderdetail = new OrderAdminDetailViewDto
                    {
                        Id = order.Id,
                        CustomerEmail = order.CustomerEmail,
                        CustomerName = order.CustomerName,
                        CustomerCity = order.CustomerCity,
                        CustomerPhone = order.CustomerPhone,
                        HomeAddress = order.HomeAddress,
                        OrderStatus = order.OrderStatus,
                        OrderDate = order.OrderDate,
                        OrderProducts = order.OrderItems.Select(oi => new CartViewDto
                        {
                            ProductId = oi.ProductId,
                            ProductName = oi.products.ProductName,
                            Price = oi.products.Price,
                            Quantity = oi.Quantity,
                            TotalAmount = oi.TotalPrice,
                            ProductImage = HostUrl+oi.products.ProductImage,
                        }).ToList()
                    };
                    return orderdetail;
                }
                return new OrderAdminDetailViewDto();            
        }
        public async Task<List<OrderViewDto>> OrderDetailsByUserId(int userId)
        {
            var user = await _dbContextClass.Orders_ul.Include(o=>o.OrderItems).ThenInclude(p=>p.products).Where(o=>o.userId == userId).ToListAsync();
            var userorder = new List<OrderViewDto>();
            foreach(var order in user)
            {
                foreach(var data in order.OrderItems)
                {
                    var orderdetail = new OrderViewDto
                    {
                        Id = data.Id,
                        OrderDate = order.OrderDate,
                        ProductName = data.products.ProductName,
                        ProductImage = data.products.ProductImage,
                        Quantity = data.Quantity,
                        OrderId = data.OrderId,
                        TotalPrice = data.TotalPrice,
                        OrderStatus = order.OrderStatus,
                    };
                    userorder.Add(orderdetail);
                }
            }
            return userorder;
        }

        public async Task<int> GetTotalOrders()
        {
            var orders = await _dbContextClass.Orders_ul.Include(oi => oi.OrderItems).ToListAsync();
            if(orders != null)
            {
                var order = orders.SelectMany(oi => oi.OrderItems);
                var totorder = order.Sum(o => o.Quantity);
                return totorder;
            }
            return 0;
        }
        public async Task<decimal> GetTotalRevenue()
        {
            var order = await _dbContextClass.Orders_ul.Include(oi => oi.OrderItems).ToListAsync();
            if (order != null)
            {
                var orderdata = order.SelectMany(oi => oi.OrderItems);
                var total = orderdata.Sum(t => t.TotalPrice);
                return total;
            }
            return 0;
        }

        public async Task<int> GetTodaysTotalOrders()
        {
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = todayStart.AddDays(1).AddTicks(-1);
            var orders = await _dbContextClass.Orders_ul.Include(oi=>oi.OrderItems).Where(o => o.OrderDate >= todayStart && o.OrderDate <= todayEnd).ToListAsync();

            if(orders != null )
            {
                var order = orders.SelectMany(_ => _.OrderItems);
                var totorder = order.Sum(_ => _.Quantity);
                return totorder;
            }
            return 0;
        }

        public async Task<decimal> GetTodaysTotalRevenue()
        {
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = todayStart.AddDays(1).AddTicks(-1);
            var order = await _dbContextClass.Orders_ul.Include(oi => oi.OrderItems).Where(o => o.OrderDate >= todayStart && o.OrderDate <= todayEnd).ToListAsync();
            if (order != null)
            {
                var orderdata = order.SelectMany(oi => oi.OrderItems);
                var total = orderdata.Sum(t => t.TotalPrice);
                return total;
            }
            return 0;
        }

        public async Task<bool> UpdateOrderStatus(int orderID, OrderUpdateDto orderUpdate)
        {
            var order = await _dbContextClass.Orders_ul.FindAsync(orderID);
            if (order != null)
            {
                order.OrderStatus = orderUpdate.OrderStatus;
                await _dbContextClass.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
