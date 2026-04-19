using Microsoft.EntityFrameworkCore;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs.Users;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Services.Users;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class UsersEndpoints // Static Class For User Endpoints
    {
        public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
        {
            var usersGroup = app.MapGroup("/Users/"); // Create A Group For /Users Endpoints



            // Endpoint To Get The Current User's ID From The JWT Token
            usersGroup.MapGet("/GetMyID", async (HttpContext httpContext) =>
            {
                return Results.Ok(int.Parse(httpContext.User.FindFirst("userID")?.Value ?? "0"));
            }).RequireAuthorization();

            // Get All User's Games From Library
            usersGroup.MapGet("/{userID}/GetGamesFromLibrary", async (int userID, GameStoreContext dbContext, IUsersMapper usersMapper, HttpContext httpContext) =>
            {
                User? user = await dbContext.Users.FindAsync(userID);

                if (user is null)
                    return Results.NotFound(UsersErrors.UserNotFound.Description);

                var userLibrary = await dbContext.UserLibraries
                    .AsNoTracking()
                    .Where(ul => ul.UserID == userID)
                    .FirstOrDefaultAsync(ul => ul.UserID == userID);

                List<UserGameDTO> games = userLibrary!.Games.Select(ug => usersMapper.ToDTO(ug)).ToList();

                return Results.Ok(games);
            }).RequireAuthorization();

            // Add A Game To The Current User's Library
            usersGroup.MapPost("/{userID}/AddGameToLibrary", async (int userID, int gameID, GameStoreContext dbContext, HttpContext httpContext) =>
            {


                return Results.Ok("Plaseholder");
            }).RequireAuthorization();

            // Remove A Game From The Current User's Library
            usersGroup.MapDelete("/{userID}/RemoveGameFromLibrary", async (int userID, int gameID, GameStoreContext dbContext, HttpContext httpContext) =>
            {

                return Results.Ok("Plaseholder");
            }).RequireAuthorization();

            return usersGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
