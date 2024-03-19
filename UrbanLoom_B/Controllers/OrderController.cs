using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanLoom_B.Dto.OrderDto;
using UrbanLoom_B.Services.OrderService;

namespace UrbanLoom_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }


        [HttpPost("order-create")]
        [Authorize]
        public async Task<ActionResult> createOrder(long price)
        {
            try
            {
                if (price <= 0)
                {
                    return BadRequest("enter a valid money ");
                }
                var orderId = await _order.OrderCreate(price);
                return Ok(orderId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }


        [HttpPost("payment")]
        [Authorize]
        public ActionResult Payment(RazorpayDto razorpay)
        {
            try
            {
                if (razorpay == null)
                {
                    return BadRequest("razorpay details must not null here");
                }
                var con = _order.Payment(razorpay);
                return Ok(con);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }




        ///   BUY FROM CART ///

        [HttpPost("PLACE ORDER (CART)")]
        [Authorize]
        public async Task<ActionResult> PlaceOrder([FromBody]OrderRequestDto orderRequestDto)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splittoken = token.Split(" ");
                var jwt = splittoken[1];

                if (jwt == null || orderRequestDto == null)
                {
                    return BadRequest();
                }
                await _order.CreateOrderFromCart(jwt, orderRequestDto);
                return Ok("Items from cart Ordered Sucessfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrdersDetails")]
        [Authorize]
        public async Task<ActionResult> GetOrderDetails()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (jwtToken == null)
                {
                    return BadRequest();
                }
                return Ok(await _order.GetOrderDtails(jwtToken));

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// ADMIN ///

        [HttpGet("GET ALL ORDERS")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminnGetAllOrders()
        {
            var Allorders = await _order.AdminGetAllOrders();
            if(Allorders == null)
            {
                return BadRequest("No orders recorded");
            }
            return Ok(Allorders);
        }

        [HttpGet("GET DETAIL OF A SINGLE ORDER")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminGetOrderDetails(int orderid)
        {
            var OrderDetails = await _order.GetDetailedOrderDetailsByOrderID(orderid);
            if(OrderDetails == null)
            {
                return BadRequest("No orders recorded by this orderid");
            }
            return Ok(OrderDetails);
        }

        [HttpGet("USER ORDERS")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UserOrder(int userid)
        {
            var order = await _order.OrderDetailsByUserId(userid);
            if(order == null)
            {
                return BadRequest("No orders recorded by this user id");
            }
            return Ok(order);
        }

        [HttpGet("TOTAL ORDERS")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> TotalOrders()
        {
            var order = await _order.GetTotalOrders();
            if (order == null)
            {
                return BadRequest("No orders recorded");
            }
            return Ok(order);
        }

        [HttpGet("TOTAL REVENUE")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> TotalRevenue()
        {
            var tot = await _order.GetTotalRevenue();
            if (tot == 0)
            {
                return BadRequest("No sales recorded");
            }
            return Ok(tot);
        }

        [HttpGet("TODAYS TOTAL ORDERS")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> TodaysTotalOrders()
        {
            var tot = await _order.GetTodaysTotalOrders();
            if (tot == 0)
            {
                return BadRequest("No sales recorded");
            }
            return Ok(tot);
        }

        [HttpGet("TODAYS TOTAL REVENUE")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> TodaysTotalRevenue()
        {
            var tot = await _order.GetTodaysTotalRevenue();
            if (tot == 0)
            {
                return BadRequest("No sales recorded");
            }
            return Ok(tot);
        }

        [HttpPut("UPDATE ORDER STATUS")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> OrderUpdate(int orderid, OrderUpdateDto orderUpdateDto)
        {
            var order = await _order.UpdateOrderStatus(orderid, orderUpdateDto);
            if (order == null)
            {
                return BadRequest("No orders recorded by this orderid");
            }
            return Ok(order);
        }
    }
}
