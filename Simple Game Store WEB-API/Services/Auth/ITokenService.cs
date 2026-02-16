using Simple_Game_Store_WEB_API.Entities;

namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates A JWT Access Token For The Specified User.
        /// </summary>
        /// <returns>The Generated JWT Access Token (String).</returns>
        public string GenerateAccessToken(User user);
    }
}
