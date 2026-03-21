namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public interface IValueHasher
    {
        /// <summary>
        /// Hashes The Provided Value Using A Secure Hashing Algorithm (e.g., Argon2, BCrypt, PBKDF2).
        /// </summary>
        /// <returns>The Hashed Value (String).</returns>
        public Task<string> HashAsync(string value);
        /// <summary>
        /// Verifies The Provided Value Against The Stored Hashed Value. Returns True If The Value Is Valid, Otherwise False.
        /// </summary>
        /// <returns>True If The Value Is Valid, Otherwise False.</returns>
        public Task<bool> VerifyAsync(string value, string hashedValue);
    }
}
