using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public interface IGameMapper
    {
        /// <summary>
        /// Converts CreateGameDTO To Game Entity
        /// </summary>
        /// <returns>Game</returns>
        public Game ToEntity(CreateGameDTO createGameDTO);
        /// <summary>
        /// Converts UpdateGameDTO To Game Entity
        /// </summary>
        /// <returns>Game</returns>
        public Game ToEntity(UpdateGameDTO updateGameDTO);



        /// <summary>
        /// Converts Game Entity To GameSummaryDTO
        /// </summary>
        /// <returns>GameSummaryDTO</returns>
        public GameSummaryDTO ToSummaryDTO(Game game);
        /// <summary>
        /// Converts Game Entity To GameDetailsDTO
        /// </summary>
        /// <returns>GameDetailsDTO</returns>
        public GameDetailsDTO ToDetailsDTO(Game game);
    }
}
