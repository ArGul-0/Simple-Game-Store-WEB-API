using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.Entities;

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
            usersGroup.MapGet("/{userID}/GetGamesFromLibrary", async (int userID, GameStoreContext dbContext, HttpContext httpContext) =>
            {

                return Results.Ok("Plaseholder");
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
