using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService tokenService;
        private readonly IValueHasher valueHasher;
        private readonly GameStoreContext dbContext;

        public AuthService(IValueHasher valueHasher, ITokenService tokenService, GameStoreContext dbContext)
        {
            this.tokenService = tokenService;
            this.valueHasher = valueHasher; 
            this.dbContext = dbContext;
        }

        public async Task<Result<string>> Login(string email, string password)
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return Result<string>.Failure(AuthErrors.InvalidCredentials); // Validate Input

            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email); // Find The User By Email
            if (user is null)
                return Result<string>.Failure(AuthErrors.InvalidCredentials); // User Not Found

            bool passwordMatches = await valueHasher.VerifyAsync(password, user.HashedPassword); // Verify The Password
            if (!passwordMatches)
                return Result<string>.Failure(AuthErrors.InvalidCredentials); // Invalid Password

            string token = tokenService.GenerateAccessToken(user); // Generate Access Token For The Authenticated User
            
            return Result<string>.Success(token); // Return The Token As A Result
        }

        public async Task<Result<string>> Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return Result<string>.Failure(AuthErrors.InvalidCredentials); // Validate Input

            bool userExists = await dbContext.Users.AsNoTracking().AnyAsync(u => u.Email == email); // Check If A User With The Same Email Already Exists
            if (userExists)
                return Result<string>.Failure(AuthErrors.UserAlreadyExists); // User Already Exists

            string hashedPassword = await valueHasher.HashAsync(password); // Hash The Password

            User newUser = new User
            {
                Username = username,
                Email = email,
                HashedPassword = hashedPassword,
                Library = new UserLibrary { OwnerID = 0, Games = new() } // Initialize The User's Library (OwnerID will be set by the DB after save)
            };

            await dbContext.Users.AddAsync(newUser); // Add The New User To The Database
            await dbContext.SaveChangesAsync(); // Save Changes To The Database

            string token = tokenService.GenerateAccessToken(newUser); // Generate Access Token For The New User

            return Result<string>.Success(token); // Return The Token As A Result
        }
    }
}
