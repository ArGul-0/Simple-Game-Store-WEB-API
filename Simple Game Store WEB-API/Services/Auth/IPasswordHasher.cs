namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes The Provided Password Using A Secure Hashing Algorithm (e.g., Argon2, BCrypt, PBKDF2).
        /// </summary>
        /// <returns>The Hashed Password (String).</returns>
        public string HashPassword(string password);
        /// <summary>
        /// Verifies The Provided Password Against The Stored Hashed Password. Returns True If The Password Is Valid, Otherwise False.
        /// </summary>
        /// <returns>True If The Password Is Valid, Otherwise False.</returns>
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
