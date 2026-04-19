using Microsoft.EntityFrameworkCore;
using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Services.Games;
using Simple_Game_Store_WEB_API.Services.Users;

namespace Simple_Game_Store_WEB_API.Services.UserLibrary
{
    public class UsersService : IUserService
    {
        private readonly GameStoreContext dbContext;

        public UsersService(GameStoreContext dbContext, IGamesMapper gamesMapper)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> AddGameToUserLibraryAsync(int userID, int gameID)
        {
            var user = await dbContext.Users
                .Include(u => u.UserLibrary)
                    .ThenInclude(ul => ul.Games)
                    .FirstOrDefaultAsync(u => u.ID == userID);

            if (user is null)
                return Result.Failure(UsersErrors.UserNotFound);

            var game = await dbContext.Games.FindAsync(gameID);

            if (game is null)
                return Result.Failure(UsersErrors.GameNotFound);
            else if (user.UserLibrary.Games.Any(g => g.GameID == gameID))
                return Result.Failure(UsersErrors.GameAlreadyInLibrary);

            var userGame = new UserGame
            {
                UserLibraryID = user.UserLibrary.ID,
                GameID = game.ID,
                Game = game,
                PurchasedAt = DateTime.UtcNow
            };

            dbContext.UserGames.Add(userGame);
            await dbContext.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> RemoveGameFromUserLibraryAsync(int userID, int gameID)
        {
            var user = await dbContext.Users
                .Include(u => u.UserLibrary)
                    .ThenInclude(ul => ul.Games)
                    .FirstOrDefaultAsync(u => u.ID == userID);

            if (user is null)
                return Result.Failure(UsersErrors.UserNotFound);

            var affected = await dbContext.UserGames
                .Where(ug => ug.UserLibraryID == user.UserLibrary.ID && ug.GameID == gameID)
                .ExecuteDeleteAsync();

            return affected == 0 ? Result.Failure(UsersErrors.GameNotFound) : Result.Success();
        }
    }
}
