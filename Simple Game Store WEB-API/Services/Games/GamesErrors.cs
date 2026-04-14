using Simple_Game_Store_WEB_API.Common.Results;

namespace Simple_Game_Store_WEB_API.Services.Games
{
    public static class GamesErrors
    {
        public static readonly Error GameNotFound = new("GameNotFound", "The specified game was not found."); // Error For When A Game Is Not Found
        public static readonly Error GenreNotFound = new("GenreNotFound", "The specified genre was not found."); // Error For When A Genre Is Not Found
    }
}
