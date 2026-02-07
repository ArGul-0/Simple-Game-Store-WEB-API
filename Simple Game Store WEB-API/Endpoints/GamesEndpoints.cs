using Simple_Game_Store_WEB_API.Validators;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class GamesEndpoints // Static Class For Games Endpoints
    {
        const string GetAllGamesEndpointName = "GetAllGames"; // Constant For The Get All Games Endpoint Name
        const string GetGameByIDEndpointName = "GetGameByID"; // Constant For The Get Game By ID Endpoint Name
        const string CreateGameEndpointName = "CreateGame"; // Constant For The Create Game Endpoint Name
        const string UpdateGameEndpointName = "UpdateGame"; // Constant For The Update Game Endpoint Name
        const string DeleteGameEndpointName = "DeleteGame"; // Constant For The Delete Game Endpoint Name

        /// <summary>
        /// Maps The Games Endpoints To The Web Application
        /// </summary>
        /// <remarks>
        /// This Method Sets Up The Following Endpoints Under The /Games Route:
        /// - GET /Games: Retrieve All Games
        /// - GET /Games/{ID}: Retrieve A Specific Game By ID
        /// - POST /Games: Create A New Game
        /// - PUT /Games/{ID}: Update An Existing Game
        /// - DELETE /Games/{ID}: Delete A Game
        /// </remarks>
        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var gamesGroup = app.MapGroup("/Games"); // Create A group For /Games Endpoints



            // GET All Games
            gamesGroup.MapGet("/", async (GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                var games = await dbContext.Games
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .Include(g => g.Genre)
                    .Select(g => gameMapper.ToSummaryDTO(g))
                    .ToListAsync();

                return Results.Ok(games);
            }).WithName(GetAllGamesEndpointName);

            // GET Game
            gamesGroup.MapGet("/{ID}", async (int ID, GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                Game? game = await dbContext.Games
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .FirstOrDefaultAsync(g => g.ID == ID);

                return game is not null ? Results.Ok(gameMapper.ToDetailsDTO(game)) : Results.NotFound();
            }).WithName(GetGameByIDEndpointName);

            // POST Game
            gamesGroup.MapPost("/", async (CreateGameDTO newGame, GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                Game game = gameMapper.ToEntity(newGame);
                game.Genre = await dbContext.Genres.FindAsync(newGame.GenreID);

                dbContext.Games.Add(game);

                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(GetGameByIDEndpointName, new { ID = game.ID }, gameMapper.ToDetailsDTO(game));
            }).AddEndpointFilter<FluentValidationEndpointFilter<CreateGameDTO>>(); // Add Validation Filter

            // PUT Game
            gamesGroup.MapPut("/{ID}", async (int ID, UpdateGameDTO updatedGame, GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                Game? game = await dbContext.Games.AsNoTracking().FirstOrDefaultAsync(g => g.ID == ID);

                if (game is null)
                    return Results.NotFound();

                game = gameMapper.ToEntity(updatedGame);
                game.Genre = await dbContext.Genres.FindAsync(updatedGame.GenreID);
                game.ID = ID;

                dbContext.Games.Update(game);

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }).WithName(UpdateGameEndpointName).AddEndpointFilter<FluentValidationEndpointFilter<UpdateGameDTO>>(); // Add Validation Filter

            // DELETE Game
            gamesGroup.MapDelete("/{ID}", async (int ID, GameStoreContext dbContext) =>
            {
                var affected = await dbContext.Games
                .Where(g => g.ID == ID)
                .ExecuteDeleteAsync();

                return affected == 0 ? Results.NotFound() : Results.NoContent();
            }).WithName(DeleteGameEndpointName);

            return gamesGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
