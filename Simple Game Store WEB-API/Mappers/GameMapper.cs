using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Mappers
{
    public class GameMapper : IGameMapper
    {
        public Game ToEntity(CreateGameDTO createGameDTO) // Converts CreateGameDTO To Game Entity
        {
            return new Game
            {
                Name = createGameDTO.Name,
                GenreID = createGameDTO.GenreID,
                Price = createGameDTO.Price,
                ReleaseDate = createGameDTO.ReleaseDate
            };
        }

        public Game ToEntity(UpdateGameDTO updateGameDTO) // Converts UpdateGameDTO To Game Entity
        {
            return new Game
            {
                Name = updateGameDTO.Name,
                GenreID = updateGameDTO.GenreID,
                Price = updateGameDTO.Price,
                ReleaseDate = updateGameDTO.ReleaseDate
            };
        }



        public GameSummaryDTO ToSummaryDTO(Game game) // Converts Game Entity To GameSummaryDTO
        {
            return new GameSummaryDTO(
                ID: game.ID,
                Name: game.Name,
                Genre: game.Genre!.Name,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate
            );
        }
        public GameDetailsDTO ToDetailsDTO(Game game) // Converts Game Entity To GameDetailsDTO
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
