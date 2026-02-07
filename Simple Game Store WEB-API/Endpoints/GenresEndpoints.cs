using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class GenresEndpoints // Static Class For Genres Endpoints
    {
        const string GetGenreEndpointName = "GetGenre"; // Constant For The Get Genre Endpoint Name

        /// <summary>
        /// Maps The Genres Endpoints To The Web Application
        /// </summary>
        /// <remarks>
        /// This Method Sets Up The Following Endpoints Under The /Genres Route:
        /// - GET /Genres: Retrieve All Genres
        /// </remarks>
        public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
        {
            var genresGroup = app.MapGroup("/Genres"); // Create A group For /Genres Endpoints



            // GET All Genres
            genresGroup.MapGet("/", async (GameStoreContext dbContext, IGenreMapper genreMapper) =>
            {
                var genres = await dbContext.Genres
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .Select(g => genreMapper.ToDTO(g))
                    .ToListAsync();

                return Results.Ok(genres);
            });

            // Get Genre
            genresGroup.MapGet("/{ID}", async (int ID, GameStoreContext dbContext, IGenreMapper genreMapper) =>
            {
                Genre? genre = await dbContext.Genres
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .FirstOrDefaultAsync(g => g.ID == ID);

                return genre is not null ? Results.Ok(genreMapper.ToDTO(genre)) : Results.NotFound();
            }).WithName(GetGenreEndpointName);

            return genresGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
