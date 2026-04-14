using Microsoft.EntityFrameworkCore;
using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;

namespace Simple_Game_Store_WEB_API.Services.Games
{
    public class GamesService : IGamesService
    {
        private readonly GameStoreContext dbContext;
        private readonly IGameMapper gameMapper;

        public GamesService(GameStoreContext dbContext, IGameMapper gameMapper)
        {
            this.dbContext = dbContext;
            this.gameMapper = gameMapper;
        }

        public async Task<Result<GameDetailsDTO>> CreateGameAsync(CreateGameDTO gameDetailsDTO)
        {
            Game game = gameMapper.ToEntity(gameDetailsDTO);
            game.Genre = await dbContext.Genres.FindAsync(gameDetailsDTO.GenreID);

            dbContext.Games.Add(game);

            await dbContext.SaveChangesAsync();

            return Result<GameDetailsDTO>.Success(gameMapper.ToDetailsDTO(game));
        }
        public async Task<Result> UpdateGameAsync(int ID, UpdateGameDTO updatedGame)
        {
            Game? existingGame = await dbContext.Games.AsNoTracking().FirstOrDefaultAsync(g => g.ID == ID);

            if (existingGame is null)
                return Result.Failure(GamesErrors.GameNotFound);

            existingGame = gameMapper.ToEntity(updatedGame);
            var genre = await dbContext.Genres.FindAsync(updatedGame.GenreID);

            if (genre is null)
                return Result.Failure(GamesErrors.GenreNotFound);

            existingGame.Genre = genre;
            existingGame.ID = ID;

            dbContext.Games.Update(existingGame);

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteGameAsync(int ID)
        {
            var affected = await dbContext.Games.
                Where(g => g.ID == ID).
                ExecuteDeleteAsync();

            return affected == 0 ? Result.Failure(GamesErrors.GameNotFound) : Result.Success();
        }
    }
}
