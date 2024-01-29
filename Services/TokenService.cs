using Microsoft.IdentityModel.Tokens;
using Orange_Portfolio_BackEnd.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Orange_Portfolio_BackEnd.Services
{
    public class TokenService
    {
        private readonly string Secret;

        public TokenService(IConfiguration configuration)
        {
            Secret = configuration["SecretKey"];

            if (string.IsNullOrEmpty(Secret))
            {
                throw new ApplicationException("Secret key configuration is not set.");
            }
        }

        // Function to generate the jwt token
        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        // Function to identify the user ID through the token
        public string GetIdByToken(HttpContext httpContext)
        {
            var userIdClaim = httpContext.User.FindFirst("userId");

            if (userIdClaim != null) return userIdClaim.Value;
            else throw new InvalidOperationException("User ID not found in JWT token.");
        }
    }
}
