using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UrbanLoom_B.JWT
{
    public class Jwt : IJwt
    {
        private readonly IConfiguration _configuration;
        private readonly string SecretKey;

        public Jwt(IConfiguration configuration)
        {
            _configuration = configuration;
            SecretKey = _configuration["Jwt:Key"];
        }
        public int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.UTF8.GetBytes(SecretKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };


            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);


            if (validatedToken is not JwtSecurityToken jwtToken)
            {
                throw new SecurityTokenException("Invalid JWT token.");
            }


            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);


            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new SecurityTokenException("Invalid or missing user ID claim.");
            }

            return userId;
        }

    }
}
