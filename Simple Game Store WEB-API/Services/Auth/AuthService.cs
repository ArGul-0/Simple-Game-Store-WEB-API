using Simple_Game_Store_WEB_API.Services.Auth.Results;
using Simple_Game_Store_WEB_API.Entities;

namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IValueHasher valueHasher;
        private readonly ITokenService tokenService;

        public AuthService(IValueHasher valueHasher, ITokenService tokenService)
        {
            this.valueHasher = valueHasher;
            this.tokenService = tokenService;   
        }



        public async Task<AuthServiceResult> Login(string email, string password)
        {
            return AuthServiceResult.Success; // Implement The Actual Login Logic Later, This Is Just A Placeholder To Allow The Application To Run Without Errors For Now.
        }

        public async Task<AuthServiceResult> Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return AuthServiceResult.InvalidCredentials; // Return InvalidCredentials If Any Of The Required Fields Are Missing Or Empty

            string hashedPassword = await valueHasher.HashAsync(password); // Hash The Password
            string hashedEmail = await valueHasher.HashAsync(email); // Hash The Email

            User newUser = new User
            {
                Username = username,
                HashedEmail = hashedEmail,
                HashedPassword = hashedPassword
            };

            string token = tokenService.GenerateAccessToken(newUser); // Generate Access Token For The New User

            return AuthServiceResult.Success;
        }
    }
}
