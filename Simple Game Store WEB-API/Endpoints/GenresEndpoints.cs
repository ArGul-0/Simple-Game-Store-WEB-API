using Simple_Game_Store_WEB_API.Services.Genres;
using Simple_Game_Store_WEB_API.Common.Results;
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
        const string UpdateGenreEndpointName = "UpdateGenre"; // Constant For The Update Genre Endpoint Name
        const string DeleteGenreEndpointName = "DeleteGenre"; // Constant For The Delete Genre Endpoint Name

        /// <summary>
        /// Maps The Genres Endpoints To The Web Application
        /// </summary>
        /// <remarks>
        /// This Method Sets Up The Following Endpoints Under The /Genres Route:
        /// - GET /Genres: Retrieve All Genres
        /// - GET /Genres/{ID}: Retrieve A Specific Genre By ID
        /// - POST /Genres: Create A New Genre
        /// - PUT /Genres/{ID}: Update An Existing Genre By ID
        /// - DELETE /Genres/{ID}: Delete A Genre By ID (Only If No Games Are Associated With It)
        /// </remarks>
        public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
        {
            var genresGroup = app.MapGroup("/Genres"); // Create A group For /Genres Endpoints



            // GET All Genres
            genresGroup.MapGet("/", async (GameStoreContext dbContext, IGenresMapper genreMapper) =>
            {
                var genres = await dbContext.Genres
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .Select(g => genreMapper.ToDTO(g))
                    .ToListAsync();

                return Results.Ok(genres);
            }).WithName(GetAllGenresEndpointName);

            // Get Genre
            genresGroup.MapGet("/{ID}", async (int ID, GameStoreContext dbContext, IGenresMapper genreMapper) =>
            {
                Genre? genre = await dbContext.Genres
                    .AsNoTracking() // Avoid Tracking For Read-Only Operation, Improves Performance
                    .FirstOrDefaultAsync(g => g.ID == ID);

                return genre is not null ? Results.Ok(genreMapper.ToDTO(genre)) : Results.NotFound();
            }).WithName(GetGenreByIDEndpointName);

            // Create Genre
            genresGroup.MapPost("/", async (CreateGenreDTO createGenreDTO, IGenreService genreService) =>
            {
                Result<GenreDTO> result = await genreService.CreateGenreAsync(createGenreDTO);

                return Results.CreatedAtRoute(GetGenreByIDEndpointName, new { ID = result.value.ID }, result.value);
            }).WithName(CreateGenreEndpointName).RequireAuthorization(); // Require Authorization For Creating Genres, Only Authenticated Users Can Create Genres

            // Update Genre
            genresGroup.MapPut("/{ID}", async (int ID, UpdateGenreDTO updatedGenreDTO, IGenreService genreService) =>
            {
                Result result = await genreService.UpdateGenreAsync(ID, updatedGenreDTO);

                if(result.IsFailure)
                {
                    return result.Error.Code switch
                    {
                        "GenreNotFound" => Results.NotFound(result.Error.Description),

                        _ => Results.BadRequest(result.Error.Description)
                    };
                }

                return Results.NoContent();
            }).WithName(UpdateGenreEndpointName).RequireAuthorization(); // Require Authorization For Updating Genres, Only Authenticated Users Can Update Genres

            // Delete Genre
            genresGroup.MapDelete("/{ID}", async (int ID, IGenreService genreService) =>
            {
                Result result = await genreService.DeleteGenreAsync(ID);

                if(result.IsFailure)
                {
                    return result.Error.Code switch
                    {
                        "GenreNotFound" => Results.NotFound(result.Error.Description),
                        "GenreHasAssociatedGames" => Results.BadRequest(result.Error.Description),

                        _ => Results.BadRequest(result.Error.Description)
                    };
                }

                return Results.NoContent();
            }).WithName(DeleteGenreEndpointName).RequireAuthorization(); // Require Authorization For Deleting Genres, Only Authenticated Users Can Delete Genres

            return genresGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
