using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Services.Games
{
    public class GamesService : IGamesService
    {
        public Task<Result<GameDetailsDTO>> CreateGameAsync(CreateGameDTO gameDetailsDTO)
        {
            throw new NotImplementedException();
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
