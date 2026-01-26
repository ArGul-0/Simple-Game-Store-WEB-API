using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public class GameMapper : IGameMapper
    {
        public Game ToEntity(CreateGameDTO createGameDTO) // Converts CreateGameDTO to Game entity
        {
            return new Game
            {
                Name = createGameDTO.Name,
                GenreID = createGameDTO.GenreID,
                Price = createGameDTO.Price,
                ReleaseDate = createGameDTO.ReleaseDate
            };
        }

        public GameDetailsDTO ToDTO(Game game) // Converts Game entity to GameDetailsDTO
        {
            return new GameDetailsDTO(
                ID: game.ID,
                Name: game.Name,
                GenreID: game.GenreID,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate
            );
        }
    }
}
