using Simple_Game_Store_WEB_API.Common.Results;

namespace Simple_Game_Store_WEB_API.Services.Users
{
    public static class UsersErrors
    {
        public static Error UserNotFound = new Error("UserNotFound", "The specified user was not found.");
        public static Error GameNotFound = new Error("GameNotFound", "The specified game was not found.");
        public static Error GameAlreadyInLibrary = new Error("GameAlreadyInLibrary", "The specified game is already in the user's library.");
    }
}
