namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public class Argon2PasswordHasher : IValueHasher
    {
        public string Hash(string password)
        {
            throw new NotImplementedException();
        }

        public bool Verify(string password, string hashedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
