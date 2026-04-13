using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;

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

        public Task<Result> DeleteGameAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateGameAsync(UpdateGameDTO gameDetailsDTO)
        {
            throw new NotImplementedException();
        }
    }
}
