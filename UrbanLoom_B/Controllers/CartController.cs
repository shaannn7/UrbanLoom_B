using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UrbanLoom_B.Services.CartService;

namespace UrbanLoom_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cart;
        public CartController(ICart cart)
        {
            _cart = cart;
        }

        [HttpGet("CART-ITEMS")]
        [Authorize]
        public async Task<ActionResult> GetCartItems()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var tokensplit = token.Split(' ');
                var jwt = tokensplit[1];
                return Ok(await _cart.GetCartItems(jwt));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ADD TO CART")]
        [Authorize]
        public async Task<ActionResult> AddtoCart(int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var tokensplit = token.Split(' ');
                var jwt = tokensplit[1];
                await _cart.AddToCart(jwt, productid);
                return Ok("Item Added to cart");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("QTY INC")]
        [Authorize]
        public async Task<ActionResult> QtyInc(int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var tokensplit = token.Split(' ');
                var jwt = tokensplit[1];
                await _cart.QuantityINC(jwt, productid);
                return Ok("Quamtity increased");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("QTY DEC")]
        [Authorize]
        public async Task<ActionResult> QtyDec(int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var tokensplit = token.Split(' ');
                var jwt = tokensplit[1];
                await _cart.QuantityDEC(jwt, productid);
                return Ok("Quamtity Decreased");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DELETE CART")]
        [Authorize]
        public async Task<ActionResult> DeleteCart(int productid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var tokensplit = token.Split(' ');
                var jwt = tokensplit[1];
                await _cart.DeleteCart(jwt, productid);
                return Ok("CartItem Removed sucessfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
