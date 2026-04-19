using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;

namespace Simple_Game_Store_WEB_API.Services.UserLibrary
{
    public class UsersService : IUserService
    {
        private readonly GameStoreContext dbContext;

        public UsersService(GameStoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> AddGameToLibrary(int userID, int gameID)
        {


            return Result.Success(); // Plaseholder
        }

        public async Task<Result> RemoveGameFromLibrary(int userID, int gameID)
        {
            
            return Result.Success(); // Plaseholder
        }
    }
}
