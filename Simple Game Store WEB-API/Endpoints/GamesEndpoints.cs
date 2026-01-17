using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class GamesEndpoints // Static class for Games endpoints
    {
        private static readonly List<GameDTO> games = // In-memory list of games
        [
            new GameDTO(1, "Gta 5", "Action-Adventure", 59.99m, new DateOnly(2013, 9, 17)),
            new GameDTO(2, "Watch Dogs 1", "Action-Adventure", 39.99m, new DateOnly(2014, 5, 26)),
            new GameDTO(3, "Cities: Skylines", "Management", 29.99m, new DateOnly(2015, 3, 10)),
            new GameDTO(4, "Fran Bow", "Horror", 4.99m, new DateOnly(2015, 8, 27)),
            new GameDTO(5, "Cry of Fear", "Horror", 0m, new DateOnly(2013, 4, 25)),
            new GameDTO(6, "Sally Face", "Adventure", 14.99m, new DateOnly(2016, 8, 16)),
            new GameDTO(7, "Silent Hill 2 Remake", "Horror-Adventure" ,29.99m, new DateOnly(2024, 10, 8))
        ];

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
            gamesGroup.MapGet("/", () => games);

            // GET Game
            gamesGroup.MapGet("/{ID}", (int ID) =>
            {
                var game = games.FirstOrDefault(g => g.ID == ID);

                return game is not null ? Results.Ok(game) : Results.NotFound();
            }).WithName(GetGameEndpointName);

            // POST Game
            gamesGroup.MapPost("/", (CreateGameDTO newGame) =>
            {
                GameDTO game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price ?? 0m,
                    newGame.ReleaseDate ?? DateOnly.MinValue
                    );

                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new { ID = game.ID }, game);
            });

            // PUT Game
            gamesGroup.MapPut("/{ID}", (int ID, UpdateGameDTO updatedGame) =>
            {
                var gameIndex = games.FindIndex(g => g.ID == ID);

                if (gameIndex == -1)
                {
                    return Results.NotFound();
                }

                GameDTO game = new(
                    ID,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price ?? 0m,
                    updatedGame.ReleaseDate ?? DateOnly.MinValue
                    );
                games[gameIndex] = game;

                return Results.NoContent();
            });

            // DELETE Game
            gamesGroup.MapDelete("/{ID}", (int ID) =>
            {
                var gameIndex = games.FindIndex(g => g.ID == ID);

                if (gameIndex == -1)
                {
                    return Results.NotFound();
                }

                games.RemoveAt(gameIndex);

                return Results.NoContent();
            });

            return gamesGroup; // Return the group for further configuration if needed
        }
    }
}
