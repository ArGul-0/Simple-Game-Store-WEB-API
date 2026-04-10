using Simple_Game_Store_WEB_API.Common.Results;

namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public static class AuthErrors
    {
        public static readonly Error InvalidCredentials = new Error("InvalidCredentials", "Email or password is incorrect.");
        public static readonly Error UserAlreadyExists = new Error("UserAlreadyExists", "A user with the provided email already exists.");
    }
}
