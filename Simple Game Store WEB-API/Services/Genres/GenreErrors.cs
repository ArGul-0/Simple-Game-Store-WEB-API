using Simple_Game_Store_WEB_API.Common.Results;

namespace Simple_Game_Store_WEB_API.Services.Genres
{
    public class GenreErrors
    {
        public static readonly Error GenreNotFound = new("GenreNotFound", "The specified genre was not found."); // Error For When A Genre Is Not Found
        public static readonly Error AnyGameWithThisGenreExist = new("AnyGameWithThisGenreExist", "Cannot delete genre because there are games associated with it."); // Error For When Trying To Delete A Genre That Has Games Associated With It
    }
}
