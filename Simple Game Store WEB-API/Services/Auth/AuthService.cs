using Simple_Game_Store_WEB_API.Common.Results;
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



        public async Task<string> Login(string email, string password)
        {
            return string.Empty; // Implement The Actual Login Logic Later, This Is Just A Placeholder To Allow The Application To Run Without Errors For Now.
        }

        public async Task<Result<string>> Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(username)); // Soon Create A Custom Exception For This, This Is Just A Placeholder To Allow The Application To Run Without Errors For Now.

            string hashedPassword = await valueHasher.HashAsync(password); // Hash The Password
            string hashedEmail = await valueHasher.HashAsync(email); // Hash The Email

            User newUser = new User
            {
                Username = username,
                HashedEmail = hashedEmail,
                HashedPassword = hashedPassword
            };

            string token = tokenService.GenerateAccessToken(newUser); // Generate Access Token For The New User

            return Task.FromResult(Result<string>.Success(token)); // Return The Token As A Result
        }
    }
}
