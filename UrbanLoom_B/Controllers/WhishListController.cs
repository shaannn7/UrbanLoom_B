using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanLoom_B.Services.WhishListService;

namespace UrbanLoom_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhishListController : ControllerBase
    {
        private readonly IWhishList _whishList;
        public WhishListController(IWhishList whishList)
        {
            _whishList = whishList;
        }

        [HttpGet("WHISHLIST")]
        [Authorize]
        public async Task<ActionResult> GetWhiashlist()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwttoken = splitToken[1];
                return Ok(await _whishList.GetWhishList(jwttoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ADD WHISHLIST")]
        [Authorize]
        public async Task<ActionResult> AddWhishlist(int productID)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splittoken = token.Split(' ');
                var jwttoken = splittoken[1];
                var isExist = await _whishList.AddToWhishList(jwttoken, productID);
                if (!isExist)
                {
                    return BadRequest("item already in the whishList");
                }
                return Ok("Item Adeed to WhishList");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DELETE WHISHLIST")]
        [Authorize]
        public async Task<ActionResult> DeleteWhishList(int productID)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splittoken = token.Split(' ');
                var jwttoken = splittoken[1];
                await _whishList.RemoveWhishList(jwttoken, productID);
                return Ok("Removed Product from WhishList");
            }catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
