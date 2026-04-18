using Simple_Game_Store_WEB_API.Common.Results;

namespace Simple_Game_Store_WEB_API.Services.UserLibrary
{
    public class UserService : IUserService
    {
        public async Task<Result> AddGameToLibrary(int userID, int gameID)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveGameFromLibrary(int userID, int gameID)
        {
            throw new NotImplementedException();
        }
    }
}
