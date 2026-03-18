namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public class AuthService : IAuthService
    {
        public async Task Login(string username, string password)
        {
        }

        public async Task Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }


        }
    }
}
