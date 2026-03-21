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

        public async Task Login(string username, string password)
        {
        }

        public async Task Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username, email, and password cannot be empty.");
            }

            string hashedPassword = await valueHasher.HashAsync(password); // Hash The Password
            string hashedEmail = await valueHasher.HashAsync(email); // Hash The Email


        }
    }
}
