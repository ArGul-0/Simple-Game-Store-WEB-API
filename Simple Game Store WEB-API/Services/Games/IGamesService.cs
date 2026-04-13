using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;
using Simple_Game_Store_WEB_API.Mappers;

namespace Simple_Game_Store_WEB_API.Services.Games
{
    public interface IGamesService
    {
        public Task<Result<GameDetailsDTO>> CreateGameAsync(CreateGameDTO gameDetailsDTO);
        public Task<Result> UpdateGameAsync(UpdateGameDTO gameDetailsDTO);
        public Task<Result> DeleteGameAsync(int ID);
    }
}
