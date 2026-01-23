using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public interface IGameMapper
    {
        public Game ToEntity(CreateGameDTO createGameDTO);
        public GameDTO ToDTO(Game game);
    }
}
