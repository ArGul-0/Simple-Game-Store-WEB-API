using Simple_Game_Store_WEB_API.Common.Results;

namespace Simple_Game_Store_WEB_API.Services.Users
{
    public static class UsersErrors
    {
        public static Error UserDontHavePermissionToPerformAction = new Error("UserDontHavePermissionToPerformAction", "You don't have permission to perform this action.");
    }
}
