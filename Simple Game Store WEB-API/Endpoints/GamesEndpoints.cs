using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class GamesEndpoints // Static Class For Games Endpoints
    {
        const string GetGameEndpointName = "GetGame"; // Constant For The Get Game Endpoint Name

        /// <summary>
        /// Maps The Games Endpoints To The Web Application
        /// </summary>
        /// <remarks>
        /// This Method Sets Up The Following Endpoints Under The /Games Route:
        /// </remarks>
        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var gamesGroup = app.MapGroup("/Games"); // Create A group For /Games Endpoints



            // GET All Games
            gamesGroup.MapGet("/", (GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                var games = dbContext.Games
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .Include(g => g.Genre)
                    .Select(g => gameMapper.ToSummaryDTO(g))
                    .ToList();

                return Results.Ok(games);
            });

            // GET Game
            gamesGroup.MapGet("/{ID}", (int ID, GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                Game? game = dbContext.Games
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .FirstOrDefault(g => g.ID == ID);

                return game is not null ? Results.Ok(gameMapper.ToDetailsDTO(game)) : Results.NotFound();
            }).WithName(GetGameEndpointName);

            // POST Game
            gamesGroup.MapPost("/", (CreateGameDTO newGame, GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                Game game = gameMapper.ToEntity(newGame);
                game.Genre = dbContext.Genres.Find(newGame.GenreID);

                dbContext.Games.Add(game);

                dbContext.SaveChanges();

                return Results.CreatedAtRoute(GetGameEndpointName, new { ID = game.ID }, gameMapper.ToDetailsDTO(game));
            });

            // PUT Game
            gamesGroup.MapPut("/{ID}", (int ID, UpdateGameDTO updatedGame, GameStoreContext dbContext, IGameMapper gameMapper) =>
            {
                Game? game = dbContext.Games.AsNoTracking().FirstOrDefault(g => g.ID == ID);

                if (game is null)
                    return Results.NotFound();

                game = gameMapper.ToEntity(updatedGame);
                game.Genre = dbContext.Genres.Find(updatedGame.GenreID);
                game.ID = ID;

                dbContext.Games.Update(game);

                dbContext.SaveChanges();

                return Results.NoContent();
            });

            // DELETE Game
            gamesGroup.MapDelete("/{ID}", (int ID, GameStoreContext dbContext) =>
            {
                var affected = dbContext.Games
                .Where(g => g.ID == ID)
                .ExecuteDelete();

                return affected == 0 ? Results.NotFound() : Results.NoContent();
            });

            return gamesGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
