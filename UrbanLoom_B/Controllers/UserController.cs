using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrbanLoom_B.Dto.LoginDto;
using UrbanLoom_B.Dto.RegisterDto;
using UrbanLoom_B.Entity;
using UrbanLoom_B.Services.UserService;

namespace UrbanLoom_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly IConfiguration _configuration;

        public UserController(IUserService user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        [HttpPost("REGISTER")]
        public async Task<ActionResult> Register([FromBody]RegisterDto registerDto)
        {
            try
            {
                var userIsExist = await _user.RegisterUser(registerDto);
                if (!userIsExist)
                {
                    return BadRequest("User is already exist");
                }
                return Ok("Registerd Sucessfully");
            }
            catch(Exception ex) 
            {
                return StatusCode(500,$"An error occured{ex}");
            }
        }

        [HttpPost("LOGIN")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var userexist = await _user.Login(loginDto);
                if(userexist == null)
                {
                    return BadRequest("Email or Password is incorrect");
                }
                if (userexist.isBlocked)
                {
                    return BadRequest("Access Denied");
                }
                bool validatePassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, userexist.Password);
                if (!validatePassword)
                {
                    return BadRequest("Password does'nt match");
                }
                string token = GenerateToken(userexist);
                return Ok(new {Token = token , email = userexist.Mail, name=userexist.Name });

                
            }catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("USERS")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await _user.GetUsers());
            }catch(Exception ex)
            {
                return StatusCode(500,ex);
            }
        }

        [HttpGet("USER = {id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult>GetUserbyID(int id)
        {
            try
            {
                var user = await _user.GetUserByID(id);
                if(user == null)
                {
                    return BadRequest("User not found");
                }
                return StatusCode(200,user);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPut("BLOCK-USER")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> BlockUser(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return NotFound();
                }

                var status = await _user.BlockUser(userId);
                if (!status)
                {
                    return BadRequest("USer Not Found");
                }
                return Ok("Blocked");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpPut("UNBLOCK-USER")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UnblockUser(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return NotFound();
                }

                var status = await _user.UnblockUser(userId);
                if (!status)
                {
                    return BadRequest("USer Not Found");
                }
                return Ok("Unblocked");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        private string GenerateToken(User users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
            new Claim(ClaimTypes.Name, users.Name),
            new Claim(ClaimTypes.Role, users.Role),
            new Claim(ClaimTypes.Email, users.Mail),
        };

            var Token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)

            );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
