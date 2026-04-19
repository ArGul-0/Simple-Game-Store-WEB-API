using Simple_Game_Store_WEB_API.Common.Results;

namespace Simple_Game_Store_WEB_API.Services.UserLibrary
{
    public interface IUserService
    {
        /// <summary>
        /// Adds A Game To A User's Library, If The Game Is Not Already In The Library And If The User Have Sufficient Permissions.
        /// </summary>
        /// <param name="userID">The ID Of The User.</param>
        /// <param name="gameID">The ID Of The Game To Add.</param>
        /// <returns>A Result Object Indicating Success Or Failure.</returns>
        public Task<Result> AddGameToUserLibraryAsync(int userID, int gameID);
        /// <summary>
        /// Removes A Game From A User's Library, If The Game Exists In The Library And If The User Have Sufficient Permissions.
        /// </summary>
        /// <param name="userID">The ID Of The User.</param>
        /// <param name="gameID">The ID Of The Game To Remove.</param>
        /// <returns>A Result Object Indicating Success Or Failure.</returns>
        public Task<Result> RemoveGameFromUserLibraryAsync(int userID, int gameID);
    }
}
