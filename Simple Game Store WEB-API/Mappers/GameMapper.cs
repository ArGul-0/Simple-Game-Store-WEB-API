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

        public GameDTO ToDTO(Game game) // Converts Game entity to GameDTO
        {
            return new GameDTO(
                ID: game.ID,
                Name: game.Name,
                Genre: game.Genre!.Name,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate
            );
        }
    }
}
