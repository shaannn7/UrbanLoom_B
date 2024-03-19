using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.Dto.CartDtos;
using UrbanLoom_B.Dto.OrderDto;
using UrbanLoom_B.Entity;
using UrbanLoom_B.JWT;
using Order = UrbanLoom_B.Entity.Order;

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


        public async Task<string> OrderCreate(long price)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            Random random = new Random();
            string TrasactionId = random.Next(0, 1000).ToString();
            input.Add("amount", Convert.ToDecimal(price) * 100);
            input.Add("currency", "INR");
            input.Add("receipt", TrasactionId);

            string key = _configuration["Razorpay:KeyId"];
            string secret = _configuration["Razorpay:KeySecret"];

            RazorpayClient razorpay = new RazorpayClient(key, secret);

            Razorpay.Api.Order orderrazorpay = razorpay.Order.Create(input);
            var OrderId = orderrazorpay["id"].ToString();

            return OrderId;
        }

        public bool Payment(RazorpayDto razorpay)
        {
            if (razorpay == null ||
                razorpay.razorpay_payment_id == null ||
                razorpay.razorpay_order_id == null ||
                razorpay.razorpay_signature == null)
            {
                return false;
            }

            RazorpayClient client = new RazorpayClient(_configuration["Razorpay:KeyId"], _configuration["Razorpay:KeySecret"]);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", razorpay.razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay.razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay.razorpay_signature);

            Utils.verifyPaymentSignature(attributes);

            return true;
        }

        ///   BUY FROM CART ///
        public async Task<bool> CreateOrderFromCart(string token, OrderRequestDto orderRequest)
        {
           
            int UserId = _jwt.GetUserIdFromToken(token);
            if(UserId == null)
            {
                throw new Exception("user id is not valid");
            }
            if (orderRequest.TransactionId == null && orderRequest.OrderString == null)
            {
                return false;
            }
            var cartorder = await _dbContextClass.Cart_ul.Include(ci=>ci.cartitem).ThenInclude(p=>p.products).FirstOrDefaultAsync(i=>i.UserId == UserId);
            var order = new Order
            {
                userId = UserId,
                OrderDate = DateTime.Now,
                CustomerCity = orderRequest.CustomerCity,
                CustomerEmail = orderRequest.CustomerEmail,
                CustomerPhone = orderRequest.CustomerPhone,
                HomeAddress = orderRequest.HomeAddress,
                CustomerName = orderRequest.CustomerName,
                TransactionId = orderRequest.TransactionId,
                OrderString = orderRequest.OrderString,
                OrderStatus = "Pending",
                OrderItems = cartorder.cartitem.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.Quantity * ci.products.Price
                }).ToList()
            };
            _dbContextClass.Orders_ul.AddAsync(order);
            _dbContextClass.Cart_ul.Remove(cartorder);
            await _dbContextClass.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderViewDto>> GetOrderDtails(string token)
        {
            int userId = _jwt.GetUserIdFromToken(token);

            if (userId == null)
            {
                throw new Exception("user id not valid");
            }

            var orders = await _dbContextClass.Orders_ul
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.products)
                .Where(o => o.userId == userId)
                .ToListAsync();

            var orderDetails = new List<OrderViewDto>();

            foreach (var order in orders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var orderDetail = new OrderViewDto
                    {
                        Id = orderItem.Id,
                        OrderDate = order.OrderDate,
                        ProductName = orderItem.products.ProductName,
                        ProductImage = HostUrl + orderItem.products.ProductImage,
                        Quantity = orderItem.Quantity,
                        TotalPrice = orderItem.TotalPrice,
                        OrderId = order.OrderString,
                        OrderStatus = order.OrderStatus
                    };

                    orderDetails.Add(orderDetail);
                }
            }
            return orderDetails;
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
                TransactionId = o.TransactionId,
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
                   TransactionId = order.TransactionId,
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
                        OrderId = data.OrderId.ToString(),
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
            var orders = await _dbContextClass.Orders_ul.CountAsync();
           return orders;
        }
        public async Task<decimal> GetTotalRevenue()
        {
            var order = await _dbContextClass.Orderitems_ul.SumAsync(x=>x.TotalPrice);
            return order;
        }

        public async Task<int> GetTodaysTotalOrders()
        {
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = todayStart.AddDays(1).AddTicks(-1);
            var orders = await _dbContextClass.Orders_ul.Where(o => o.OrderDate >= todayStart && o.OrderDate <= todayEnd).CountAsync();
            return orders;
        }

        public async Task<decimal> GetTodaysTotalRevenue()
        {
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = todayStart.AddDays(1).AddTicks(-1);
            var Revenue = await _dbContextClass.Orderitems_ul.Where(o => o.order.OrderDate >= todayStart && o.order.OrderDate <= todayEnd).SumAsync(x=>x.TotalPrice);
            return Revenue;
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
