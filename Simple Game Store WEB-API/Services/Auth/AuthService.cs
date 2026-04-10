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



        public async Task<Result<string>> Login(string email, string password)
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return Result<string>.Failure(AuthErrors.InvalidCredentials); // Validate Input

            return Result<string>.Failure(new Error("Login Failed", "Invalid email or password.")); // Placeholder For Now, Soon Will Implement The Actual Login Logic That Checks The Credentials Against The Database And Returns A Token If Successful.
        }

        public async Task<Result<string>> Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return Result<string>.Failure(AuthErrors.InvalidCredentials); // Validate Input

            string hashedPassword = await valueHasher.HashAsync(password); // Hash The Password

            User newUser = new User
            {
                Username = username,
                Email = email,
                HashedPassword = hashedPassword
            };

            string token = tokenService.GenerateAccessToken(newUser); // Generate Access Token For The New User

            return Result<string>.Success(token); // Return The Token As A Result
        }
    }
}
