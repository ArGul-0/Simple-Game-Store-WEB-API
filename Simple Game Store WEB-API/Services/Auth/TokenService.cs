using Simple_Game_Store_WEB_API.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var secretKey = configuration["JwtOptions:SecretKey"] ?? throw new Exception("JWT Secret Key is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userID", user.ID.ToString()),
                    new Claim("username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(int.Parse(configuration["JwtOptions:ExpirationHours"] ?? throw new Exception("JWT Expiration Hours is not configured."))),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
