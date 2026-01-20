using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;

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
            gamesGroup.MapGet("/", (GameStoreContext dbContext) => dbContext.Games.ToList());

            // GET Game
            gamesGroup.MapGet("/{ID}", (int ID, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.Find(ID);

                return game is not null ? Results.Ok(game) : Results.NotFound();
            }).WithName(GetGameEndpointName);

            // POST Game
            gamesGroup.MapPost("/", (CreateGameDTO newGame, GameStoreContext dbContext) =>
            {
                var game = new Game
                {
                    Name = newGame.Name,
                    Genre = dbContext.Genres.Find(newGame.GenreID),
                    GenreID = newGame.GenreID,
                    Price = newGame.Price,
                    ReleaseDate = newGame.ReleaseDate
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                GameDTO gameDTO = new(
                    game.ID,
                    game.Name,
                    game.Genre!.Name,
                    game.Price,
                    game.ReleaseDate
                    );

                return Results.CreatedAtRoute(GetGameEndpointName, new { ID = game.ID }, gameDTO);
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
