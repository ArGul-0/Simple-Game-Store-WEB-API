using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class GamesEndpoints // Static class for Games endpoints
    {
        const string GetGameEndpointName = "GetGame"; // Constant for the Get Game endpoint name

        /// <summary>
        /// Maps the Games endpoints to the WebApplication.
        /// </summary>
        /// <remarks>
        /// This method sets up the following endpoints under the /Games route:
        /// </remarks>
        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var gamesGroup = app.MapGroup("/Games"); // Create a group for /Games endpoints



            // GET All Games
            gamesGroup.MapGet("/", (GameStoreContext dbContext) =>
            {
                var games = dbContext.Games
                    .AsNoTracking().Select(gameDTO => new GameDTO(
                        gameDTO.ID,
                        gameDTO.Name,
                        gameDTO.Genre!.Name,
                        gameDTO.Price,
                        gameDTO.ReleaseDate
                        ))
                    .ToList();

                return Results.Ok(games);
            });

            // GET Game
            gamesGroup.MapGet("/{ID}", (int ID, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.Find(ID);

                return game is not null ? Results.Ok(game) : Results.NotFound();
            }).WithName(GetGameEndpointName);

            // POST Game
            gamesGroup.MapPost("/", (CreateGameDTO newGame, GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                Game game = gameMapper.ToEntity(newGame);
                game.Genre = dbContext.Genres.Find(newGame.GenreID);

                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute(GetGameEndpointName, new { ID = game.ID }, gameMapper.ToDTO(game));
            });

            // PUT Game
            gamesGroup.MapPut("/{ID}", (int ID, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.Find(ID);

                if (game is null)
                {
                    return Results.NotFound();
                }

                game = new Game
                {
                    Name = updatedGame.Name,
                    Genre = dbContext.Genres.Find(updatedGame.GenreID),
                    GenreID = updatedGame.GenreID,
                    Price = updatedGame.Price,
                    ReleaseDate = updatedGame.ReleaseDate
                };

                dbContext.Games.Update(game);
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            // DELETE Game
            gamesGroup.MapDelete("/{ID}", (int ID, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.Find(ID);

                if (game is null)
                {
                    return Results.NotFound();
                }

                dbContext.Games.Remove(game);
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            return gamesGroup; // Return the group for further configuration if needed
        }
    }
}
