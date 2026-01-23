using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public interface IGameMapper
    {
        /// <summary>
        /// Converts CreateGameDTO to Game entity
        /// </summary>
        /// <returns>Game</returns>
        public Game ToEntity(CreateGameDTO createGameDTO);

        /// <summary>
        /// Converts Game entity to GameDTO
        /// </summary>
        /// <returns>GameDTO</returns>
        public GameDTO ToDTO(Game game);
    }
}
