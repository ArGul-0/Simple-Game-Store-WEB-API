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

        public TokenService(IConfiguration configuration) // Constructor For TokenService, Accepting IConfiguration To Access JWT Settings From Configuration
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Generates A JWT Access Token For The Specified User, Containing Claims For User ID And Username, And Signed With A Secret Key From Configuration.
        /// </summary>
        /// <param name="user">The User For Whom The Token Is Being Generated.</param>
        /// <returns>A JWT Access Token As A String.</returns>
        public string GenerateAccessToken(User user)
        {
            var secretKey = configuration["JwtOptions:SecretKey"] ?? throw new Exception("JWT Secret Key is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var issuer = configuration["JwtOptions:Issuer"] ?? throw new Exception("JWT Issuer is not configured.");
            var audience = configuration["JwtOptions:Audience"] ?? throw new Exception("JWT Audience is not configured.");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] // Define Claims For The Token, Including User ID And Username To Allow Identification Of The User During Authentication And Authorization Processes.
                {
                    new Claim("userID", user.ID.ToString()),
                    new Claim("username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(int.Parse(configuration["JwtOptions:ExpirationHours"] ?? throw new Exception("JWT Expiration Hours is not configured."))), // Set Token Expiration To Enhance Security By Limiting The Time Window During Which A Stolen Token Could Be Used. Adjust The Expiration Time Based On Your Application's Security Requirements And User Experience Considerations.
                SigningCredentials = credentials, // Sign The Token With The Generated Credentials To Ensure Its Integrity And Authenticity During Validation.
                Issuer = issuer, // Include Issuer Claim To Ensure Token Is Validated Against The Expected Issuer During Authentication. This Helps Prevent Tokens Issued By Untrusted Sources From Being Accepted.
                Audience = audience // Include Audience Claim To Ensure Token Is Validated Against The Intended Audience During Authentication. This Enhances Security By Preventing Token Misuse In Different Contexts.
            };

            var tokenHandler = new JwtSecurityTokenHandler(); // Create A JWT Security Token Handler To Generate The Token Based On The Defined Descriptor, Which Includes Claims, Expiration, Signing Credentials, Issuer, And Audience.
            var token = tokenHandler.CreateToken(tokenDescriptor); // Generate The JWT Token Using The Token Handler And The Defined Token Descriptor, Which Encapsulates All The Necessary Information For The Token's Creation And Validation.
            var tokenString = tokenHandler.WriteToken(token); // Serialize The Generated JWT Token To A String Format That 

            return tokenString; // Return The Generated JWT Access Token As A String To Be Used For Authentication And Authorization In The Application, Allowing The User To Access Protected Resources Based On The Claims Included In The Token.
        }
    }
}
