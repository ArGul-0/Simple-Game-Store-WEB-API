using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class GenresEndpoints // Static Class For Genres Endpoints
    {
        const string GetAllGenresEndpointName = "GetAllGenres"; // Constant For The Get All Genres Endpoint Name
        const string GetGenreByIDEndpointName = "GetGenreByID"; // Constant For The Get Genre By ID Endpoint Name
        const string CreateGenreEndpointName = "CreateGenre"; // Constant For The Create Genre Endpoint Name

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
            }).WithName(GetAllGenresEndpointName);

            // Get Genre
            genresGroup.MapGet("/{ID}", async (int ID, GameStoreContext dbContext, IGenreMapper genreMapper) =>
            {
                Genre? genre = await dbContext.Genres
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .FirstOrDefaultAsync(g => g.ID == ID);

                return genre is not null ? Results.Ok(genreMapper.ToDTO(genre)) : Results.NotFound();
            }).WithName(GetGenreByIDEndpointName);

            // Create Genre
            genresGroup.MapPost("/", async (CreateGenreDTO createGenreDTO, GameStoreContext dbContext, IGenreMapper genreMapper) =>
            {
                Genre newGenre = genreMapper.ToEntity(createGenreDTO);

                dbContext.Genres.Add(newGenre);

                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(GetGenreByIDEndpointName, new { ID = newGenre.ID }, genreMapper.ToDTO(newGenre));
            }).WithName(CreateGenreEndpointName);

            return genresGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
